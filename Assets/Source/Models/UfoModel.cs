using Source.Views;

namespace Source.Models
{
	public class UfoModel : EnemyModel
	{
		public UfoView UfoView => (UfoView)View;
		
		public UfoModel(EnemyView view) : base(view)
		{
		}
	}
}