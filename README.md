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
1. [Use CSharp client sdk](https://youngmonkeys.org/ezyfox-csharp-client-sdk/)
1. [User to ezydata-mongodb](https://youngmonkeys.org/introduce-to-ezymongo/)
 
 # Unity build
 
 
 1. Clone source code
 2. Move to ```unity/Assets``` folder
 3. Clone ```ezyfox-server-csharp-client``` by commands:
 
 ```bash
git init
git remote add origin https://github.com/youngmonkeys/ezyfox-server-csharp-client.git
git pull origin master
```

4. Import project to ```Unity Hub```
5. Build and done

# Server-side build and run

1. Clone source code
2. Import source code (```server``` folder) into IDE (Eclipse, Intellij, Netbean)
3. Run file [SpaceShooterStartup](https://github.com/youngmonkeys/freechat/blob/master/server/freechat-startup/src/main/java/com/tvd12/freechat/FreechatStartup.java)

# License

Apache License, Version 2.0