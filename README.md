# ![](jira.png) JIRA Wallboard Screensaver

Show your JIRA Wallboards as a screensaver on your information radiator machine.

[![Build Status](https://travis-ci.org/jonscheiding/jira-wallboard-screensaver.svg?branch=master)](https://travis-ci.org/jonscheiding/jira-wallboard-screensaver)

## Prerequisites

1. .NET Framework 4.6.2 or higher

## Installation 

1. Download the lastest release from the [releases page](https://github.com/jonscheiding/jira-wallboard-screensaver/releases/latest).  Unzip the .scr file to somewhere on your computer.
2. Right click on the .scr file and click "Install".

## Configuration

1. Enter the URL of your JIRA instance.
2. If your wallboard requires authentication to view, click the Login to JIRA button (![person_outline](Jira.WallboardScreensaver/Resources/ic_person_outline.png)).  Enter your credentials.  (Your password will not be stored; the configuration will retrieve a login cookie from JIRA.)
3. Click the Load Dashboards button (![search](Jira.WallboardScreensaver/Resources/ic_search.png)).  Choose your dashboard, and click Save.

##  Uninstallation

1. Delete the .scr file (it will automatically disappear from your Windows screensaver settings).
2. Delete the registry key **HKEY_CURRENT_USER\SOFTWARE\Jira Wallboard Screensaver**.