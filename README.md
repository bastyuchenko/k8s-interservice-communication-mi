# k8s-interservice-communication-mi

https://learn.microsoft.com/en-us/dotnet/api/overview/azure/service-to-service-authentication  
https://jwt.io/


Accounts in any organizational directory (Any Azure AD directory - Multitenant)  
iss: https://sts.windows.net/88b28885-115e-4d82-b554-b8785399306e/  

Accounts in this organizational directory only (Default Directory only - Single tenant)  
iss: https://sts.windows.net/88b28885-115e-4d82-b554-b8785399306e/  

Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)  
"iss": "https://login.microsoftonline.com/88b28885-115e-4d82-b554-b8785399306e/v2.0"
