namespace Platform::Data
{
    using namespace Platform::Interfaces;
    
    /// @brief Manages state and control flow for write operation handlers.
    /// @tparam TStorage The storage type that defines LinkAddressType and other related types.
    /// 
    /// This class provides a stateful wrapper around write operation handlers, allowing
    /// for early termination (break) conditions and result accumulation. It's commonly
    /// used in operations that process multiple links and need to support interruption.
    template<typename TStorage>
    struct WriteHandlerState
    {
        /// @brief The address value that signals a break/stop condition.
        typename TStorage::LinkAddressType Break;
        
        /// @brief The current result value, initialized to 0.
        typename TStorage::LinkAddressType Result = 0;
        
        /// @brief The underlying handler function for write operations.
        std::function<typename TStorage::LinkAddressType(std::vector<typename TStorage::LinkAddressType>, std::vector<typename TStorage::LinkAddressType>)> Handler;

        /// @brief Constructs a WriteHandlerState with specified control values and handler.
        /// @param $continue The value representing continue/success state.
        /// @param $break The value that triggers break/stop condition.
        /// @param handler The function to handle write operations.
        WriteHandlerState(typename TStorage::LinkAddressType $continue, typename TStorage::LinkAddressType $break, auto&& handler) :
            Result{$continue}, Break{$break}, Handler{handler} {}

        /// @brief Applies the result and checks for break condition.
        /// @param result The result value to process.
        /// @return The current Result value.
        /// 
        /// If the result equals Break, sets Result to Break and nullifies Handler
        /// to prevent further processing.
        typename TStorage::LinkAddressType Apply(typename TStorage::LinkAddressType result)
        {
            if (Break == result)
            {
                Result = Break;
                Handler = nullptr;
            }
            return Result;
        }

        /// @brief Handles arguments by invoking the stored handler function.
        /// @param args Arguments to forward to the handler function.
        /// @return Result value after applying the handler or current Result if handler is null.
        /// 
        /// If Handler is null (due to break condition), returns current Result.
        /// Otherwise, invokes Handler with forwarded arguments and applies the result.
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
