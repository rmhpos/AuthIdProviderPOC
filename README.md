# Pros and cons Keycloak:
+ SSO +
+ Multiple apps(apis, web, mobile) - Microservices, etc +
+ Federation(LDAP, AD) +
+ Authentication is left to Keycloak, only needs authorization management +
+ Has admin UI, where you control roles, claims, users, etc. +
+ Provides Login, Logout, Registration, MFA(2FA), 3-rd party authentication providers(google, azure, facebook, etc.) ui redirects. No need to create your own pages and logic for those. Customization of the UI is possible.
+ Provides api endpoints that Login, Logout, Register, etc. if required in the app +
+ Provides the cutting edge technologies for a more secure app(Authorization code + PKCE for web apps specifically, has other flows each dealing with a specific challange) +
+ Works best with a BFF in between the web UI(browser) and the api for the highest standards of application security
+ Allows for an easy and most importantly scalable management of auhtorization access with Roles claims, Clients, Client Scopes and Users. +
+ Ideal for being part of a multi-tenant solution, as the access rights to the different tenants(Central, Stores) can happen in a signle centralized place +
+ Blazor has razor componenets specifically created to work with 3rd party Identity providers +
!!!NO NEED TO CREATE OUR OWN SYSTEM FOR THAT!!! +

- Additional infrastructure(container with Keycloak and a speperate DB)
- A bit of a learning curve with figuring out the terminology and the structure of the keycloak system(Reoles, Clients, Clients Scopes, Sessions, Mappers, Users, etc.)
- When info about the user is required will need to somehow be able to propagate from Keycloak to ui client and api.

# Pros and cons .Net Identity:
+ Simple to setup for the most rudementory authentication and authorization. +
+ Gives you the tooling to create you own atuhentication and authorization flows. By default it uses cookies. +
+ Comes with out-of-the-box mapping of api endpoints that allow you to manage authentication and authorization.  +Couldn't make them work on a short notice, but with a new solution shouldn't be a problem. +
+ There is a way to create a more robust Role-Based Authorization system.(Roles and custom claims called permissions) - Will need more time to figure that out +
+ Has support for token authentication and authorization +
+ If the system is created as a black box(the user is severaly restricted in what he is allowed to do and see) this is a good solution +
+ Could plug in to the authorization state management pipes +

- Plugging to the authorization state management pipes will take additional time to figure it out -
- Keeping the token secure is left up to us(This will required additional time and complexity added to the code since a new system that manages that has to be added). Time -
- If we want to make it secure to industry standards we'll need to recreate our own authentication flows(authorization code flow, PKCE, Client Credentials flow, etc.). Time -
- No SSO support. Have to create a complicated system additionaly for that(OpenIdDict). Time -
- No multiple app support. Needs a lot of rework to figure that out.(OpenIdDict). Time -
- Each api holds it's own db tables that are responsible for the management of the auth and auth. More things to care about and manage. -
- If we want a more fine-grained functionality to manage claim-based authorization we'll need to create a custom ui to maange that(ui pages). Time -

# Pros and cons of basic authentication + authorization permissions config files:
+ The system will be entirely created by our team which will upgrade the developers skills +
+ If we don't want ot be flexible then it will be very simple and straight-forward. Fast +
+ If can put security a bit lower on the priority scale(don't need to implement complex authentication and authirzation flows). Fast +

- There is strong advice from security proffesionals that in the case of Auth systems developers shouldn't try to create systems out of scrach especially for the highest of industry standards as the flows are complex. Time -
- The system needs to be flexible we will in the end recreate something similar to EntraId or Keycloak(users having the right to create roles, edit, update; add, remove, update permissions to roles; create complex roles(roles of roles)). Time -
- If we want the system to use the best Security standards we'll still need to use at least Identity for authentication and for the authorization ecosystem that it provides. Time -
