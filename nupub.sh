#!/bin/bash

APIKEY=802E277B3B75455E841FA307D82D514C
VERSION=$1
SERVICEURL=http://nuget.starbendersystems.com/nuget/nuget

nuget push Mapbox.Platform/bin/Debug/Mapbox.Platform.$VERSION.nupkg $APIKEY -Source $SERVICEURL
nuget push Mapbox.Map/bin/Debug/Mapbox.Map.$VERSION.nupkg $APIKEY -Source $SERVICEURL
nuget push Mapbox.Geocoding/bin/Debug/Mapbox.Geocoding.$VERSION.nupkg $APIKEY -Source $SERVICEURL
nuget push Mapbox.Utils/bin/Debug/Mapbox.Utils.$VERSION.nupkg $APIKEY -Source $SERVICEURL
nuget push Mapbox.Directions/bin/Debug/Mapbox.Directions.$VERSION.nupkg $APIKEY -Source $SERVICEURL

