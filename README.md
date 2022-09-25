# k8s-interservice-communication-mi

https://learn.microsoft.com/en-us/azure/active-directory/develop/access-tokens  
https://learn.microsoft.com/en-us/dotnet/api/overview/azure/service-to-service-authentication  

# Overal description
There are 2 services: 
## Service1 
Service1 sends a request to Service 2. Service 1 generate token base on (see [appsettings.json](https://github.com/bastyuchenko/k8s-interservice-communication-mi/blob/main/Service1/appsettings.json)):  
__Resource__ - it is a resource in the Azure AD. It can be [Exposed API](https://learn.microsoft.com/en-us/azure/active-directory/develop/quickstart-configure-app-expose-web-apis) of an arbitrary registered application in Azure AD.  
__TenantId__ - Azure AD tanant Id.  
__ClientId__ - ClientId of the registered app in Azure AD that you will use to acquire token  
__ClientSecret__ - ClientId of the registered app in Azure AD that you will use to acquire token  

The registered application that contains ClientId and ClientSecret and another registered application that contains exposed API (see __Resource__) should be in the same Azure AD tenant.

## Service2
Service2 should:  
* receive a request from Service1
* Authenticate request (means validate token based on AddJwtBearer method)
* Authorize token (using IAuthorizationHandler we check claim "group")

### Authentication
We use:  
__Audience__ - it's ClientId of a registered application with the Expose API. This exposed API we used in __Resource__ configuration of the Service1.  
__TenantId__ - Azure AD tanant Id.  

You can re-check you auth settings in the following way:  
Catch AccessToken sent from Service1 (you can use debugging in IDE for this purpose). Put AccessToken to https://jwt.io/.
[ValidAudience](https://github.com/bastyuchenko/k8s-interservice-communication-mi/blob/main/Service2/Program.cs#L21) should be equal to "aud".  
[ValidIssuer](https://github.com/bastyuchenko/k8s-interservice-communication-mi/blob/main/Service2/Program.cs#L22)  should be equal to "iss".  

```
Depending on the Supported account types "iss" should distinguish:  

Accounts in any organizational directory (Any Azure AD directory - Multitenant)  
iss: https://sts.windows.net/88b28885-115e-4d82-b554-b8785399306e/  

Accounts in this organizational directory only (Default Directory only - Single tenant)  
iss: https://sts.windows.net/88b28885-115e-4d82-b554-b8785399306e/  

Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)  
"iss": "https://login.microsoftonline.com/88b28885-115e-4d82-b554-b8785399306e/v2.0"
```


# Deploy infrastructure in Azure

## Authentication providing  
Register a new application in Azure AD for Service1  
![image](https://user-images.githubusercontent.com/6165551/192064247-f6e2743f-0f6c-4c3b-92df-2d3f784fbeee.png)
Add "New client secret" in the section "Certificate and secrets"  


## Register a new application in AzureAD for:  
* to have the value that will be specified in __Resource__ in Service1 ("Exposed API" name will be used) and __Audience__ in Service2 (ClientId)
![image](https://user-images.githubusercontent.com/6165551/192065553-8f7d56de-bbb7-46f5-80ec-2e139a6965ec.png)

## Expose API  
Expose an API -> Application ID URI -> Set
![image](https://user-images.githubusercontent.com/6165551/192066270-53eefd09-03e5-4075-b8b5-63ec879b091e.png)


## Authentication providing
1. We need to add "groups" claim  
Interservice.Communication.Resource -> Token configuration -> Add groups claim  
> :warning: Please pay attension, logically it seams we have to add group claim for __Interservice.Communication.Service1__ but it doesn't work. In fact we need to add token claim for registerd app which we use for __"Resource" (Audience)__ parameter in config.  

![image](https://user-images.githubusercontent.com/6165551/192167315-4b7a2ec6-0729-4e5f-bf79-e185f351f4c3.png)


Create AzureAD group. Add this service Interservice.Communication.Service1 (especially his Service Principle) to new group. This group ID will be chenked on the Service2.
![image](https://user-images.githubusercontent.com/6165551/192067209-9c4f8108-8336-4727-81d6-0153a5761d43.png)
