namespace Source.Interfaces
{
	public interface IPlayerInputProvider
	{
		float Rotation { get; }
		bool IsMove { get; }
		bool IsFire { get; }
	}
}