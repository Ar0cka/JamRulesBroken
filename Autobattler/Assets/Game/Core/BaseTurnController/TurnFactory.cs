namespace Game.Core.BaseTurnController
{
    public class TurnFactory
    {
        public BaseTurnController CreateTurnController<TType>() where TType : BaseTurnController, new()
        {
            var turnController = new TType();
            
            turnController.InitializeTurnController();

            return turnController;
        }
    }
}