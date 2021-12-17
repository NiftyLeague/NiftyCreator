var NiftyLeagueLib = {
	$NiftyLeague: {
		TryParseJson: function (str) {
			try {
				return JSON.parse(str);
			} catch (e) {
				return str;
			}
		},
	},
	DispatchEvent: function (eventName, detail) {
		eventNameStr = Pointer_stringify(eventName);
		detailStr = Pointer_stringify(detail);
		const event = new CustomEvent(eventNameStr, {
			'detail': NiftyLeague.TryParseJson(detailStr)
		});
		window.dispatchEvent(event);
	},
	SubmitTraits: function (traits, name, callback) {
		traitsStr = Pointer_stringify(traits);
		nameStr = Pointer_stringify(name);
		callbackStr = Pointer_stringify(callback);
		const detail = {
			traits: NiftyLeague.TryParseJson(traitsStr),
			callback: function (result) {
				window.unityInstance.SendMessage(nameStr, callbackStr, result);
			}
		};
		const event = new CustomEvent('SubmitTraits', {
			'detail': detail
		});
		console.log(event);
		window.dispatchEvent(event);
	},
	StartAuthentication: function (name, callback) {
		nameStr = Pointer_stringify(name);
		callbackStr = Pointer_stringify(callback);
		const detail = {
			callback: function (result) {
				window.unityInstance.SendMessage(nameStr, callbackStr, result);
			}
		};
		const event = new CustomEvent('StartAuthentication', {
			'detail': detail
		});
		window.dispatchEvent(event);
	},
	SignMessage: function (address, message, password, name, callback) {
		addressStr = Pointer_stringify(address);
		messageStr = Pointer_stringify(message);
		passwordStr = Pointer_stringify(password);
		nameStr = Pointer_stringify(name);
		callbackStr = Pointer_stringify(callback);
		try {
			ethereum.request({
				method: 'personal_sign',
				params: [messageStr, addressStr, passwordStr],
			}).then(function (sign) {
				window.unityInstance.SendMessage(nameStr, callbackStr, 'true,' + sign);
			}).catch(function (error) {
				console.log(error);
				window.unityInstance.SendMessage(nameStr, callbackStr, 'false,' + error);
			});
		} catch (error) {
			console.log(error);
			window.unityInstance.SendMessage(nameStr, callbackStr, 'false,' + error);
		}
	},
	GetRemovedTraits: function (name, callback) {
		nameStr = Pointer_stringify(name);
		callbackStr = Pointer_stringify(callback);
		const detail = {
			callback: function (result) {
				console.log(nameStr + '.' + callbackStr);
				window.unityInstance.SendMessage(nameStr, callbackStr, result);
			}
		};
		const event = new CustomEvent('GetRemovedTraits', {
			'detail': detail
		});
		window.dispatchEvent(event);
	},
	GetConfiguration: function (name, callback) {
		nameStr = Pointer_stringify(name);
		callbackStr = Pointer_stringify(callback);
		const detail = {
			callback: function (result) {
				console.log(nameStr + '.' + callbackStr);
				window.unityInstance.SendMessage(nameStr, callbackStr, result);
			}
		};
		const event = new CustomEvent('GetConfiguration', {
			'detail': detail
		});
		window.dispatchEvent(event);
	},
};

autoAddDeps(LibraryManager.library, '$NiftyLeague');
mergeInto(LibraryManager.library, NiftyLeagueLib);
