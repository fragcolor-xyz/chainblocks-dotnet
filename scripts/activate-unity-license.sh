#!/usr/bin/env bash

#
# Activate a Unity license
# based on https://github.com/game-ci/unity-activate
# MIT License
# Copyright (c) 2019-present Webber Takken <webber@takken.io>
#

echo "Requesting activation (personal license)"

# Set the license file path
FILE_PATH=UnityLicenseFile.ulf

# Copy license file from Github variables"
echo "$UNITY_LICENSE" | tr -d '\r' > $FILE_PATH

# Activate license
ACTIVATION_OUTPUT=$(unity-editor -quit -logFile /dev/stdout -manualLicenseFile $FILE_PATH)

# Store the exit code from the verify command
UNITY_EXIT_CODE=$?
# note: the exit code for personal activation is always 1
ACTIVATION_SUCCESSFUL=$(echo $ACTIVATION_OUTPUT | grep 'Next license update check is after' | wc -l)

# Set exit code to 0 if activation was successful
if [[ $ACTIVATION_SUCCESSFUL -eq 1 ]]; then
UNITY_EXIT_CODE=0
fi;

# Remove license file
rm -f $FILE_PATH

#
# Display information about the result
#
if [ $UNITY_EXIT_CODE -eq 0 ]; then
# Activation was a success
echo "Activation complete."
else
# Activation failed so exit with the code from the license verification step
echo "###########################"
echo "#         Failure         #"
echo "###########################"
echo ""
echo "Please note that the exit code is not very descriptive."
echo "Most likely it will not help you solve the issue."
echo ""
echo "To find the reason for failure: please search for errors in the log above."
echo ""
echo "Exit code was: $UNITY_EXIT_CODE"
exit $UNITY_EXIT_CODE
fi
