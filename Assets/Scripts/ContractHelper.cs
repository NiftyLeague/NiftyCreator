#if USE_NETHEREUM
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.JsonRpc.UnityClient;
#endif //#if USE_NETHEREUM
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class ContractHelper : MonoBehaviour
{
#if USE_NETHEREUM
	private static ContractHelper I;

	private void Awake()
	{
		I = this;
	}

	public static void GetCharacters(string address, Action<List<int[]>> onComplete)
	{
		I.StartCoroutine(_GetCharacters(address, onComplete));
	}

	private static IEnumerator _GetCharacters(string address, Action<List<int[]>> onComplete)
	{
		var url = "https://ropsten.infura.io/v3/532b81157636457891dcda23c9652fee";
		var contractAddress = "0x9030140b740130c4d4fd72bbe8bedc0d83356670";
		var tokensQuery = new QueryUnityRequest<AccountTokensFunc, AccountTokensFuncOutput>(url, address);

		yield return tokensQuery.Query(new AccountTokensFunc() { Owner = address }, contractAddress);
		List<int[]> characters = new List<int[]>();
		List<BigInteger> tokens = tokensQuery.Result != null ? tokensQuery.Result.Tokens : new List<BigInteger> { 1, 2 };
		if (tokensQuery.Result != null)
		{
			foreach (BigInteger token in tokensQuery.Result.Tokens)
			{
				var charTraitsQuery = new QueryUnityRequest<CharacterTraitsFunc, CharacterTraitsFuncOutput>(url, address);
				yield return charTraitsQuery.Query(new CharacterTraitsFunc() { TokenId = token }, contractAddress);
				if (charTraitsQuery.Result != null)
				{
					characters.Add(charTraitsQuery.Result.Traits.Select(t => (int)t).ToArray());
				}
			}
		}
		onComplete(characters);
	}

	public static void GetRemovedTraits(string address, Action<int[]> onComplete)
	{
		I.StartCoroutine(_GetRemovedTraits(address, onComplete));
	}

	private static IEnumerator _GetRemovedTraits(string address, Action<int[]> onComplete)
	{
		var url = "https://ropsten.infura.io/v3/532b81157636457891dcda23c9652fee";
		var contractAddress = "0x7a63b8ccdf1b056adff39ae3c11427a30d21210f";
		var query = new QueryUnityRequest<RemovedTraitsFunc, RemovedTraitsFuncOutput>(url, address);

		yield return query.Query(new RemovedTraitsFunc(), contractAddress);
		onComplete(query.Result != null ? query.Result.Traits.Select(t => (int)t).ToArray() : null);
	}


	[Function("getCharacterTraits", typeof(List<uint>))]
	public class CharacterTraitsFunc : FunctionMessage
	{
		[Parameter("uint256", "tokenId")]
		public BigInteger TokenId { get; set; }
	}

	[FunctionOutput]
	public class CharacterTraitsFuncOutput : IFunctionOutputDTO
	{
		[Parameter("uint[]")]
		public List<uint> Traits { get; set; }
	}


	[Function("getAccountTokens", typeof(List<BigInteger>))]
	public class AccountTokensFunc : FunctionMessage
	{
		[Parameter("address", "owner")]
		public string Owner { get; set; }
	}

	[FunctionOutput]
	public class AccountTokensFuncOutput : IFunctionOutputDTO
	{
		[Parameter("uint[]")]
		public List<BigInteger> Tokens { get; set; }
	}


	[Function("getRemovedTraits", typeof(List<uint>))]
	public class RemovedTraitsFunc : FunctionMessage
	{
	}

	[FunctionOutput]
	public class RemovedTraitsFuncOutput : IFunctionOutputDTO
	{
		[Parameter("uint[]")]
		public List<uint> Traits { get; set; }
	}
#endif //#if USE_NETHEREUM
}

