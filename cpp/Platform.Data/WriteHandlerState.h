namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename TStorage>
    struct WriteHandlerState
    {
        typename TStorage::LinkAddressType Break;
        typename TStorage::LinkAddressType Result = 0;
        std::function<typename TStorage::LinkAddressType(std::vector<typename TStorage::LinkAddressType>, std::vector<typename TStorage::LinkAddressType>)> Handler;

        WriteHandlerState(typename TStorage::LinkAddressType $continue, typename TStorage::LinkAddressType $break, auto&& handler) :
            Result{$continue}, Break{$break}, Handler{handler} {}

        typename TStorage::LinkAddressType Apply(typename TStorage::LinkAddressType result)
        {
            if (Break == result)
            {
                Result = Break;
                Handler = nullptr;
            }
            return Result;
        }

        typename TStorage::LinkAddressType Handle(auto&& ...args)
        {
            if(nullptr == Handler)
            {
                return Result;
            }
            else
            {
                return Apply({Handler(std::forward<decltype(args)>(args)...)});
            }
        }
    };
}
