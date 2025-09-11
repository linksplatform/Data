/// @file Platform.Data.h
/// @brief Main header file for the Platform.Data library.
/// 
/// This header includes all the essential components of the Platform.Data library,
/// which provides a flexible framework for working with links - connections or 
/// relationships between data elements. The library supports various addressing
/// schemes, link representations, and extensible operations through templates.
/// 
/// Key components included:
/// - Core interfaces (ILinks)
/// - Link addressing (LinkAddress)
/// - Configuration options (LinksOptions, LinksConstants)
/// - Extension methods (ILinksExtensions)
/// - Data conversion utilities
/// - Exception handling
///
#pragma once

#include <Platform.Ranges.h>
#include <Platform.Setters.h>
#include <Platform.Interfaces.h>

#include "WriteHandlerState.h"

#include "Numbers/Raw/AddressToRawNumberConverter.h"
#include "Numbers/Raw/RawNumberToAddressConverter.h"

#include "LinkAddress.h"
#include "Point.h"
#include "Hybrid.h"
#include "LinksConstants.h"
#include "LinksOptions.h"
#include "LinksConstantsExtensions.h"
#include "ILinks.h"

#include "ILinksExtensions.h"
