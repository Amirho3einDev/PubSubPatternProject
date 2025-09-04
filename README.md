Run Requirement:
 -Set SqlServer ConnectionString In AppSetting
 -Run Command "Update-DataBase" For Create DataBase From Migrations

 Project Description:
  -This Project Has One Api Action With Name Login That Use For Login With Username And Password
  - After Login Publish Notification Event And Subscribed By Registered Subscribers In MyJobService That Create One Subscriber In Every 10 Secounds With UniqueId
    
