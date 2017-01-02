#!/bin/bash
sharpie -tlm-do-not-submit bind --output=. --namespace=GoogleAnalytics.iOS --sdk=iphoneos10.0 ./GoogleAnalyticsServicesiOS_3.17/GoogleAnalytics/Library/*.h
mv -f ApiDefinitions.cs ApiDefinition.cs
mv -f StructsAndEnums.cs Structs.cs