using Classes;

namespace Cards.CScripts
{
    public class CardHud: GameHud
    {
        public CardHud(GameLayout gl) : base(gl)
        {
            Initialize(gl);            
        }

        public CardHud() : base()
        {
            
        }
    }
}