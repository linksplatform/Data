namespace Platform::Data
{
    class ILinksExtensions
    {
        public: static TLinkAddress Count<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, params TLinkAddress restrictions[])
            where TConstants : LinksConstants<TLinkAddress>
            => links.Count()(restrictions);

        public: static bool Exists<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, TLinkAddress link)
            where TConstants : LinksConstants<TLinkAddress>
        {
            auto constants = links.Constants;
            return constants.IsExternalReference(link) || (constants.IsInternalReference(link) && Comparer<TLinkAddress>.Default.Compare(links.Count()(LinkAddress<TLinkAddress>(link)), 0) > 0);
        }

        public: static void EnsureLinkExists<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, TLinkAddress link)
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (!links.Exists(link))
            {
                throw ArgumentLinkDoesNotExistsException<TLinkAddress>(link);
            }
        }

        public: static void EnsureLinkExists<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, TLinkAddress link, std::string argumentName)
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (!links.Exists(link))
            {
                throw ArgumentLinkDoesNotExistsException<TLinkAddress>(link, argumentName);
            }
        }

        public: static TLinkAddress Each<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, Func<IList<TLinkAddress>, TLinkAddress> handler, params TLinkAddress restrictions[])
            where TConstants : LinksConstants<TLinkAddress>
            => links.Each(handler, restrictions);

        public: static IList<TLinkAddress> GetLink<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, TLinkAddress link)
            where TConstants : LinksConstants<TLinkAddress>
        {
            auto constants = links.Constants;
            if (constants.IsExternalReference(link))
            {
                return Point<TLinkAddress>(link, constants.TargetPart + 1);
            }
            auto linkPartsSetter = Setter<IList<TLinkAddress>, TLinkAddress>(constants.Continue, constants.Break);
            links.Each(linkPartsSetter.SetAndReturnTrue, link);
            return linkPartsSetter.Result;
        }

        public: static bool IsFullPoint<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, TLinkAddress link)
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (links.Constants.IsExternalReference(link))
            {
                return true;
            }
            links.EnsureLinkExists(link);
            return Point<TLinkAddress>.IsFullPoint(links.GetLink(link));
        }

        public: static bool IsPartialPoint<TLinkAddress, TConstants>(ILinks<TLinkAddress, TConstants> &links, TLinkAddress link)
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (links.Constants.IsExternalReference(link))
            {
                return true;
            }
            links.EnsureLinkExists(link);
            return Point<TLinkAddress>.IsPartialPoint(links.GetLink(link));
        }
    };
}
