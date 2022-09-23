# k8s-interservice-communication-mi

https://learn.microsoft.com/en-us/dotnet/api/overview/azure/service-to-service-authentication  
https://jwt.io/

# Overal description
There 2 services: 
## Service1 
Service1 sends request to Service 2. Service 1 generate token base on (see [appsettings.json](https://github.com/bastyuchenko/k8s-interservice-communication-mi/blob/main/Service1/appsettings.json)):  
__Resource__ - it is resource in the Azure AD. It can be [Exposed API]() of arbitrary registered application in Azure AD.  
__TenantId__ - Azure AD tanant Id.  
__ClientId__ - ClientId of registered app in Azure AD that you will use to aquire token  
__ClientSecret__ - ClientId of registered app in Azure AD that you will use to aquire token  

The registered application that contains ClientId and ClientSecret and another registered application that contains exposed API (see __Resource__) should be in the same Azure AD tenant.

## Service2
Service2 should:  
* receive request from Service1
* Authenticate request (means validate token based on AddJwtBearer method)
* Authorize token (using IAuthorizationHandler we check claim "group")

### Authentication
We use:  
__Audience__ - it's ClientId of an registered application with exposed API that. This exposed API we used in __Resource__ configuration of the Service1.  
__TenantId__ - Azure AD tanant Id.  

You can re-check you auth settings in the following way:  
[ValidAudience](https://github.com/bastyuchenko/k8s-interservice-communication-mi/blob/main/Service2/Program.cs#L21) should be equal to "aud".  
[ValidIssuer](https://github.com/bastyuchenko/k8s-interservice-communication-mi/blob/main/Service2/Program.cs#L22)  should be equal to "iss".  

```
Depends on the Supported account types "iss" should distinguish:  

Accounts in any organizational directory (Any Azure AD directory - Multitenant)  
iss: https://sts.windows.net/88b28885-115e-4d82-b554-b8785399306e/  

Accounts in this organizational directory only (Default Directory only - Single tenant)  
iss: https://sts.windows.net/88b28885-115e-4d82-b554-b8785399306e/  

Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)  
"iss": "https://login.microsoftonline.com/88b28885-115e-4d82-b554-b8785399306e/v2.0"
```


