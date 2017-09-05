# ![](jira.png) JIRA Wallboard Screensaver

Show your JIRA Wallboards as a screensaver on your information radiator machine.

[![Build Status](https://travis-ci.org/jonscheiding/jira-wallboard-screensaver.svg?branch=master)](https://travis-ci.org/jonscheiding/jira-wallboard-screensaver)

## Installation 

1. Download the lastest release from the [releases page](releases/latest).  Put the .scr file somewhere on your computer.
2. Right click on the .scr file and click "Install".

## Configuration

1. Enter the URL of the wallboard.  You can get this by going to your JIRA Dashboard, clicking the **[•••]** menu, and clicking "View as Wallboard".
2. If your wallboard does not require authentication to view, tick the Anonymous checkbox.
3. Otherwise, enter the credentials to view the wallboard.  (Your password will not be stored; the configuration will retrieve a login cookie from JIRA.)

##  Uninstallation

1. Delete the .scr file (it will automatically disappear from your Windows screensaver settings).
2. Delete the registry key **HKEY_CURRENT_USER\SOFTWARE\Jira Wallboard Screensaver**.