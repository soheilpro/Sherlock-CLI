# Sherlock
Sherlock is a cross-platform CLI application for managing passwords and other personal information.

## Download
+ Linux: [sherlock-1.0-linux-x64.tar.gz](https://github.com/soheilpro/Sherlock/releases/download/v1.0/sherlock-1.0-linux-x64.tar.gz)
+ MacOS: [sherlock-1.0-osx-x64.tar.gz](https://github.com/soheilpro/Sherlock/releases/download/v1.0/sherlock-1.0-osx-x64.tar.gz)
+ Windows: [sherlock-1.0-win-x64.zip](https://github.com/soheilpro/Sherlock/releases/download/v1.0/sherlock-1.0-win-x64.zip)

## Other Versions
+ Web: https://github.com/soheilpro/Sherlock-Web
+ iOS: https://github.com/soheilpro/Sherlock-iOS
+ Windows GUI: https://github.com/soheilpro/Sherlock-Win

## Usage
1. Create or open a database:

```
$ sherlock MyDatabase.sdb
```

2. Set a password:

```
/> chpwd A_LONG_STRONG_PASSWORD
```

3. Add your data:

```
/> mkdir github.com
/> cd github.com
/github.com> add email soheil@example.com
/github.com> add password p@ssw0rd --secret
/github.com> show email     # Prints soheil@example.com
/github.com> clip password  # Copies password to the clipboard
```

Type `help` to see all the available commands.

4. Save changes and quit:

```
/> save
/> exit
```

## Version History
+ **1.0**
	+ Initial release.

## Contributing
Please report issues or better yet, fork, fix and send a pull request.

## Author
**Soheil Rashidi**

+ http://soheilrashidi.com
+ http://twitter.com/soheilpro
+ http://github.com/soheilpro

## Copyright and License
Copyright 2018 Soheil Rashidi.

Licensed under the The MIT License (the "License");
you may not use this work except in compliance with the License.
You may obtain a copy of the License in the LICENSE file, or at:

http://www.opensource.org/licenses/mit-license.php

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
