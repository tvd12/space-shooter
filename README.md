# space-shooter
a socket game build on ezyfox-server and unity

# Introduction 

Space Shooter is a socket game. It uses #ezyfox-server and #MongoDB for server side, #Unity and #ezyfox-server-unity client sdk for client

# It supports

- Unity

# Documentation

[https://youngmonkeys.org/space-shooter/](https://youngmonkeys.org/asset/space-shooter/)

# Guide

1. [Use embedded ezyfox-server](https://youngmonkeys.org/use-embedded-server/)
2. [Use CSharp client sdk](https://youngmonkeys.org/ezyfox-csharp-client-sdk/)
3. [User to ezydata-mongodb](https://youngmonkeys.org/introduce-to-ezymongo/)
 
 # Unity build
 
 
1. Clone source code
2. Run command: `git submodule update --init --recursive` to pull submodules
3. Import project to ```Unity Hub```
5. Build and done

# Server-side 

## Setup

1. Download and install mongodb: https://www.mongodb.com/try/download/community
2. Open mongo client, you can use `mongo` shell or [mongodb compass](https://www.mongodb.com/try/download/compass)
3. Create database `space-shooter` with username `root` and password `123456`. If you use command line, you can use command in [script.sh file](https://github.com/tvd12/space-shooter/blob/master/server-java/src/main/resources/script.sh)

## Build and Run

1. Clone source code
2. Import source code (```server``` folder) into IDE (Eclipse, Intellij, Netbean)
3. Run file [SpaceShooterStartup](https://github.com/youngmonkeys/freechat/blob/master/server/freechat-startup/src/main/java/com/tvd12/freechat/FreechatStartup.java)

# License

Apache License, Version 2.0