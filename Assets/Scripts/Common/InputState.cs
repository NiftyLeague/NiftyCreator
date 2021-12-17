public class InputState
{
	public float xAxis, yAxis, leftTrigger, rightTrigger;
	public bool aButton, bButton, xButton, yButton, up, down, left, right, start;
	public bool wasAButton, wasBButton, wasXButton, wasYButton, wasUp, wasDown, wasLeft, wasRight, wasStart;

	public bool PressedA { get { return aButton && !wasAButton; } }
	public bool PressedB { get { return bButton && !wasBButton; } }
	public bool PressedX { get { return xButton && !wasXButton; } }
	public bool PressedY { get { return yButton && !wasYButton; } }
	public bool PressedUp { get { return up && !wasUp; } }
	public bool PressedDown { get { return down && !wasDown; } }
	public bool PressedLeft { get { return left && !wasLeft; } }
	public bool PressedRight { get { return right && !wasRight; } }
	public bool PressedStart { get { return start && !wasStart; } }

	public bool ReleasedA { get { return !aButton && wasAButton; } }
	public bool ReleasedB { get { return !bButton && wasBButton; } }
	public bool ReleasedX { get { return !xButton && wasXButton; } }
	public bool ReleasedY { get { return !yButton && wasYButton; } }
	public bool ReleasedUp { get { return !up && wasUp; } }
	public bool ReleasedDown { get { return !down && wasDown; } }
	public bool ReleasedLeft { get { return !left && wasLeft; } }
	public bool ReleasedRight { get { return !right && wasRight; } }
	public bool ReleasedStart { get { return !start && wasStart; } }


	public uint GetPacked()
	{
		uint inputs = 0;
		inputs |= (aButton ? 1u : 0) << 0;
		inputs |= (bButton ? 1u : 0) << 1;
		inputs |= (xButton ? 1u : 0) << 2;
		inputs |= (yButton ? 1u : 0) << 3;
		inputs |= (up ? 1u : 0) << 4;
		inputs |= (down ? 1u : 0) << 5;
		inputs |= (left ? 1u : 0) << 6;
		inputs |= (right ? 1u : 0) << 7;
		inputs |= (start ? 1u : 0) << 8;
		return inputs;
	}

	public void SetFromPacked(uint inputs)
	{
		aButton = (inputs & 1) == 1;
		inputs >>= 1;
		bButton = (inputs & 1) == 1;
		inputs >>= 1;
		xButton = (inputs & 1) == 1;
		inputs >>= 1;
		yButton = (inputs & 1) == 1;
		inputs >>= 1;
		up = (inputs & 1) == 1;
		inputs >>= 1;
		down = (inputs & 1) == 1;
		inputs >>= 1;
		left = (inputs & 1) == 1;
		inputs >>= 1;
		right = (inputs & 1) == 1;
		inputs >>= 1;
		start = (inputs & 1) == 1;
	}
}
