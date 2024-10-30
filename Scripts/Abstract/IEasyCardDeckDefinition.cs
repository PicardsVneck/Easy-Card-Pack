using System.Collections.Generic;

namespace EasyCard
{
    public interface IEasyCardDeckDefinition
    {
        public List<EasyCard> buildDeck();

    }
}