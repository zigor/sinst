# sinst
Command line tool to install Sitecore packages to Sitecore sites

## How To Use 

This tool allows users to install a Sitecore package to a Sitecore site. The package installation requires available Sitecore backend.

Usage:

```
sinst host package [user] [password]
      -h host -p package [-u user] [-pw password] [-s]
      -host host -package package [-user user] [-password password] [-silent]
```

Parameter List:
```
        -h|-host        host            Sitecore site to connect to.

        -p|-package     package         Specifies a package path.
                                        The path can be either a file path or url

        -u|-user        user            Specifies a username for Sitecore authentication.
                                        The default value is "admin"

        -pw|-password   password        Specifies a password for Sitecore authentication.
                                        The default value is "b"

        -s|-silent      true|false      Disables prompts and resolve all items, 
                                        files and security conflicts silently.
                                        The default value is "true"

        ?|-?|-help                      Displays this help message.
```
Example:
```
  sinst.exe http://sc82 https://github.com/zigor/sitecore.assemblyBinding/releases/download/v1.0/Sitecore.AssemblyBinding-1.0.zip
```
