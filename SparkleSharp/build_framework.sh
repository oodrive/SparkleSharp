#!/bin/bash

DIR=$(dirname ${0})
echo Building sparkle from ${DIR}
cd ${DIR}
git submodule init && git submodule update
cd 3rdparty/Sparkle
xcodebuild -configuration Release -target bsdiff
xcodebuild -configuration Release -target ed25519
xcodebuild -configuration Release -target Sparkle -xcconfig ../../release.xcconfig

CodeSigningCertificate=`security find-identity -v -p codesigning | grep "Developer ID Application:" | awk -F'"' '{print $2}'`
echo Signing with certificate: \"$CodeSigningCertificate\"

# Some Sparkle dependencies did not support hardened runtime, but it can be set during code signing with "runtime" option.
# https://github.com/micahstubbs/open-dozer/issues/4

# WARNING: codsign order is important, always sign children before parents!
codesign --force --deep --verbose --timestamp --options runtime --sign "$CodeSigningCertificate" build/Release/Sparkle.framework/Versions/Current/Resources/Autoupdate.app/Contents/MacOS/fileop
codesign --force --deep --verbose --timestamp --options runtime --sign "$CodeSigningCertificate" build/Release/Sparkle.framework/Versions/Current/Resources/Autoupdate.app/Contents/MacOS/Autoupdate
codesign --force --deep --verbose --timestamp --options runtime --sign "$CodeSigningCertificate" build/Release/Sparkle.framework/Versions/Current/Resources/Autoupdate.app
codesign --force --deep --verbose --timestamp --options runtime --sign "$CodeSigningCertificate" build/Release/Sparkle.framework/Versions/Current/.

# TODO: use Sharpie to generate obj-c wrapper
#sharpie bind -o "${DIR}" -scope build/Release/Sparkle.framework/Versions/A/Headers  -namespace SparkleSharp -sdk macosx10.14 build/Release/Sparkle.framework/Versions/A/Headers/Sparkle.h
