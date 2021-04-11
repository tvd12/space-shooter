use space-shooter;
db.createUser(
   {
     user:"root",
     pwd:"123456",
     roles:[ { role: "readWrite", db: "space-shooter" }]
   }
)
