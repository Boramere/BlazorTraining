# Blazor Notes

## Questions
How do I tell what hosting model, or rendering mode has been chosen
Rendermode can be set against the Route component in App.razor
Just writing out the object, so that call the ToString method?
The hell is an Expression
What exactly is SignalR - MS developed set of services to provide communication between servers and clients. Uses websockets primarily, but can use others
Invoke/InvokeAsync methods. Why use

## Hosting Models
Server, WebAssembly or Hybrid.
Server is mostly hosted on the server, gives minimal downloads and rapid responses
More SEO friendly, as the rendering is done server side and can be indexed
Can use server resources for processing etc
Code runs exlucisvely on the server, so decompilation isn't a risk
Downsides are that it requires constant server connection, no offline mode
Each action requires server connection, hence latency can be a factor
Requires more server resources
Uses SignalR to communicate with the client/server. The downloads are basically just the components required to set up this connection

WebAssembly is mostly client side. 
Larger downloads, but can deal with slow or inconsistent connections
Can function without server connections
Also doesn't require .net capable server, can run on gibhub static pages for example
Can use Progressive Web Apps (apps built on the web that can be compiled and installed on mobile devices)
Better user reactions, no latency
Requires larger initial downloads
Requires a web api to access the database and server resources
WASM uses javascript interupts instead of SignalR. The WASM isn't allowed to update the DOM, so it raises JS events, and then JS updates

Can also use automode which uses server to begin with, and then webassembly next time you visit
This prevents the initial download time, and gives the user advantages of webassembly on next visit

Hybrid is for desktop or mobile applications

## Looking at the templates
Blazor Web Apps
Has a ASP.Net backend
Creates two projects, a front end blazor app, and back end asp api

Blazor WebAssembly Standalone App
Doesn't have an ASP.Net backend

.Net MAUI Blazor Hybrib App
Designed for building mobile/desktop apps

Razor Class Library
For building components etc

## Razor Syntax
@ for swapping to c# for just a single variable
@() for swapping to c# for a single statement. The results are converted to string
@{} for multiple lines of c#. Can mix in html here, and will sometimes require @ to covert back to c#
@code {} for a set of code lines. Can have multiple of these
Code behind pages for more detailed code

## Page and Routing
Routing can be handled by various methods
@page command for quick and easy routes. Can have multiple of these
{Variable} for using the @page command to input variables. This matches the [Parameter] tag on properties
{Variable:int} to define the variable type
Can mix up these settings in the various routes. For example /page/{StringVar} vs /page/{IntVar:int} will populate different variables
Can also use the [Route] table against the class in the code behind page

## Parameters
Parameters is a property that has the [Parameter] tag set
Be careful about naming the variable with the same text as values in html for example. This can refer to either the text of the string, or the variable name
Convention dictacts that always use the @ symbol if using the variable name
Editor required tag in the param attribute will enforce the parameter to be supplied. This will mean the property will never be null
VS will still complain about the nullability, but can be removed by using the required tag against the property. Note this is only a hint to other code that calls the component. If it's just on a page, it doesn't function as you'd expect
SupplyParameterFromQuery attribute will populate from the query string, but skip having to use the route variable to supply it. The parameter in the query string needs to be named in this case

## Dependency Inject
Singleton injection varies depending on the hosting model. With Server, a singleton is shared across all users. With WebAssembly, it is also shared across all users. But because all users is only a single one, due to running in the browser, it's functionally a single user
Scoped objects are specific to the SignalR or Socket connection. Refreshing the page in either server or webassembly will drop the objects
Transient works the same as usual, new object every time
The objects will also be dropped when moving to a page that doesn't have blazor components visible. This can cause state to be dropped when it's not expected. This is not necessarily a problem, but something to be aware of. There are various methods of solving it if needed
For Webassembly, singleton and scoped are effectively the same thing. However, it's useful to still design your DI to use the one that you would normally using in server. ie, design as if you have more than a single user
Dependcy inject is done via the @inject parameter

## Type Param
A type param is how you can do generic components for using elsewhere

## Attribute
The @attribute directive adds an attribute to a component. For example @attribute [Authorize] for the auth attribute

## Attributes
The @attributes tag can be used to insert custom attributes into a component
For example, if you have a button component, having the @attributes tag on it will allow the user of the component to inject attributes into the rendering
This means you don't have to manually apply every attribute that you want to display on your button

## @Using and _Imports
@Using directive works the same way as in c#
_Imports file is common set of using directives that affect a folder. Note that this doesn't have to be limited to using directives. The contents of this file are simply added to the components in the folder when it is transpiled. 
This means you can add other directives that apply to all components in a folder

## Layouts
@layout can be used to set the layout used by the page
Layouts can be inherited from other layouts
Layouts are a good example of using the _Imports file to import a layout for every file in a folder

## @onclick & EventCallbacks
The @onclick directive can be used to call onclick events
EventCallbacks can be used for finer grain control of event handlers

## Data binding
On way data binding we have done already, using the @VarName syntax to insert the variable value into a text box for example
The @bind directive will set up two way binding. When the value changes, the variable will be auto-updated.
@bind when used on an html element will bind to the default option. For text boxes, this is the value property.
@bind-value specifically binds to the value.
By default, the binding occurs onchange. For a text box, this is after the box is changed, AND the control loses focus
Using @bind-value:event="oninput" will change this to the oninput event
@bind-value:after can be used to fire a method on the specified event, in the case on the after event
@bind-value:get and set can be used to control the getter and setter behaviour of the binding
The InputText inbuilt blazor component is probably better than the default html one
You can also mimic the behaviour of bind by manually listening to events and updating the variables yourself
Manually creating a binding component requires three parameters. A Value, a ValueExpression, and a ValueChanged. The value is the value of the item. ValueChanged is the event callback that handles the on change events. The value expression will be investigated later

## StateHasChanged
The statehaschanged method controls when to update the UI. After a method has run, the StateHasChanged operation runs, and compares the current DOM to the newly generated one. If required, it updates the UI
Most of the time this runs automatically. However sometimes it might require triggering manually. For example, if you want to update the UI in the middle of a method, or when the code is running on a background thread
StateHasChanged will only update the component that it is called on, not any parent components
StateHasChanged is fairly slow, so only use it when you specifically need it

## Cascading Parameters
Cascading parameters allows passing the same parameter values into multiple components, without duplicating code
There are two ways to do this
The first is using the <CascadingValue> tag, that allows you to group together components and pass in the value to each
The second is to use a Cascading Model, which can be configured in app startup, and will notify any listeners of any changes to the data

## Where should I put the code
For smaller components, the best practice is keep the code in the razor file
Once it starts getting larger, generally the question is how much should be moved to services, which can be injected, rather than directly in the razor page
Finally, there are some components that will be very complex, and in this case the code should be in a code behind file. This is generally the exception however

## Lifecycle events
There are various events we can hook into when the component is created.
Component Mode rendering changes the way these are loaded, and is completely different from all the other rendering modes
Not advised to run with Component Mode for that reason

## Prerendering
Prerendering is when the document is rendered first on the server, and then sent to the client
The client then renders this again once it has receieved the document
This is useful for when you want SEO, as the robots will receive the pre-rendered document. This is generally the only reason for turning ON prerendering. Otherwise turn it off
However, rendering twice will mean that database calls are loaded twice by default. It also means that the user will see one set of data briefly, then a loading screen, then a second set
This can be controlled using the PersistentComponentState system, which allows saving of the data to JSON, and loading from there when needed
Saving and loading does need to be done manually however. The PersistentComponentState object will save this to the HTML that is sent, with a big base64 enccoded string
Prerendering can be turned off system wide by altering the Routes component in App.razor

## Render Modes
Render modes can be set globally, or per component
The project template allows this selection, but can be changed later
Globablly it is set against the Route component, which is the top level component for a project. All the other components run underneath that
The default is none, which is not interactive
InteractiveServer runs on the server, InteractiveWebAssembly runs at the client
Each of these can also have pre-rendering turned on or off, leaving five render modes in total

## Server Side Rendering
Static server side rendering means there is no interactivity, and it just renders on the server and sends to the client
Streaming SSR means that once some rendering has been done, it is sent immediately to the client. The server can then process longer running processes, render those, and then send to the client later
SSR can be very fast, due to not requiring the downloads that WebAssembly does, or the SignalR connection for Server

## WebAssembly standalone
This template has no server component, and runs entirely in the browser
This means it can be hosted on static web hosts such as github
Once the first page is loaded, it downloads the various files that are needed to run, most importantly the compiled .dll files of the c# code

## Session/Local storage
Session and local storage is a way to store information in the users browser
Session storage is specific to the current session. When the tab/window is closed, the session is ended
Local storage uses HDD space, and next time the user visits your site, the state is restored
There are a variety of packages that enable this. The most popular are the BlazoredLocalStorage and BlazoredSessionStorage. Microsoft also has one, that also allows encryption of the data
Note that both of these data stores will be purged if the user clears the cache, and if they use a different machine

## Database
When using InteractiveServer, it is possible to call the database directly, without routing through an API
The example given was using entity framework, which works exactly the same as you'd normally run EF
One thing to note is that EF only allows a single query at a time. So each time you're doing a query, ensure the context is created, query ran, and then context immediately disposed off
This will ensure that future queries can run swiftly

## Injected Service
Injected services can be built to save state. These can be singleton, scoped or transient, and work to save state as you'd expect for those durations
Events can be built and subscribed to ensure the UI is updated when the state is changed
Note that when events are subscribed to, they should be unsubscribed from when finished, but implementing IDisposable

## Built in Authentication template
The built in template adds in a bunch of extra files and options to startup
These are the boilerplate code for implementing generic authentication
Startup
	AddCascadingAuthenticationState adds a cascading parameter that contains auth state information
	IdentityUserAccessor and IdentityRedirectManager are specific to the template implementation, and only relevant for that
	AuthenticationStateProvider is a service that periodically checks if the user is logged in, and persists this to the state information
	EntityFramework can also store all our users, again not specific to Blazor
In the Routes component, it adds the AuthorizeRouteView component. This hides routes if they are not accessible, and redirects to login when not accessible
The AuthorizeView component nests components or code, and allows the dev to hide content that is not accessible. This can restrict to policies, or roles as well
This component has three sub components, Authorized that will run if the user is authorized. NotAuthorized, and Authorizing

## Auth0
Auth0 has a nuget package that allows near seamless integration with the default auth providers. There are a couple differences that need to be handle, but most just slot in

## Virtualize
The virtualize component is designed to help handle large data sets 
This component will load and unload data from the DOM when it's not being displayed on the page. This saves a bunch of memory in the browser
It also handles paging, displaying a message for no rows, and such

## NavLink
The NavLink component is a way to supply urls, and then automatically add classes based on matching criteria
This means that if your current url matches all or part of the link, then an active CSS class will be added to the <a> tag
This makes it easy to create a list of links, and have the currently active page show in a different colour for example

## Navigation Manager
The navigation manager supplys a bunch of functionalty around navigating to different pages
It can create the anchor tags required to navigate around, and supply different behaviours around this
It can prevent leaving the page
It can return details of various urls, and the query strings that are supplied, and you can add to the query parameters
There is an OnLocationChanging event, which can be used to write code when the location is changing
Finally, the navigation manager can be used to refresh components that are not interactive. This can mimic an interactive experience on a component that doesn't need interactivity beyond that one function

## Error Boundary
The error boundary component captures errors inside the UI rendering
This can give access to exceptions, and allow custom display

## Navigation Lock
The navigation lock component can handle a user navigating away from the page, used to handle lack of saving and such
When navigating to an external site, the web browser handles the messaging. The dev cannot change it
When navigating internally, the message CAN be handled manually
Note that when using this component, the entire site needs to be interactive, it won't work with just the component

## QuickGrid
The quick grid is just like it sounds, adds a fairly light grid to the page
This grid can handle filtering and pagination, but requires a moderate amount of setup
This grid can be enhanced for a more full feature experience

## HeadContext / Pagetitle
These components are designed to work around the head tag being rendered before the components are
These tags allow additional tags to be added to the head
It works because in the App.Razor page, there is the HeadOutlet tag that is rendered, and these components interact with that

## Section/Section outlet
A SectionOutlet component can be added to any component, and it allows other components to set the content of that sectionoutlet
This can be used in navigation menus for example, so that child content can add things to the nav
This also works downstream, so a top level component can work with content from child components
The sections can be located by either name or ID
Note that isolated CSS won't be applied to content added using this method, only non-isolated will work

## Router
The router component is the one that does all the routing. Oddly enough
There are tags for NotFound, and for Routing. The latter handles displaying a loading page when the router is finding the way around

## EditForm
The EditForm component is designed to work like filling out a form
It can bind to a model, and has various functions around what happens when the form is submitted, if it's valid or not and such

## Validation
Blazor uses MS Data Annotations to control model validation
The DataAnnotationsValidator component will enable validation for a particular EditForm
THe ValidationSummary component will show all of the validation issues that have arisen in the form
A ValidationMessage will show the errors specific to a particular control

## The controls
InputText for text boxes, and InputTextArea for areas
InputCheckBox for checkboxes
InputDate for date selection
InputFile for uploading files
InputNumber for hanlding numbers, including ints and decimals
InputRadioGroup for grouping, and the InputRadio for individual radio options
InputSelect for drop downs

## Creating a form control
You can create your own components to use in place of the inbuilt ones
For example, the default inputtext control doesn't have a label on it, so we can add it in
However, note that rather than using the Bind directive, ensure you use the Value/ValueChanged/ValueExpression trio
Otherwise it'll all bind to the Value property on the model, rather than the correct property, and likely won't work correctly
Although these three events are needed in the component, using the binding just needs the @bind-Value property to work correctly

## SSR Forms
For most purposes, SSR is sufficient for forms
There are a couple of tricks to make it perform in the same way as InteractiveServer/Webassembly, but without the performance hit
The [SupplyParameterFromForm] tag is required on the model to ensure that the form binds properly
The Enhance directive on the EditForm ensures that the form submits with the same feeling as Interactive. ie the scrollbar doesn't change location
It does require the EditForm to have a FormName directive

## Templated Components
Templated components are custom components that incorporate RenderFragments
This allows the caller to pass text, html, and code to the component
This allows strong customization for the component

## HTML renderer
Blazor has a HTML renderer that can be called manually
This effectively allows Blazor to run Blazor, and generate and render code
The renderer needs an ILoggerFactory and a servicecollection to start up
It can then render html manually, and output it where needed
The (MarkupString) control can tell the normal renderer that the following string is html, and to render it properly. Otherwise it'll be escaped

## Renderfragment from method
When implementing components, they all inherit from ComponentBase
This means that they all run the various lifetime events each time
This comes with overhead, and if you're not using them, then that is performance that is lost
In most cases, this isn't an issue. However if you're rendering thousands of them, in a massive list for example, this is not recommended
You can create a method that returns a render fragment instead, and use this instead of the component
Generally you'd want to make this static, so it can be accessed wherever you need

## Third party components
There are a lot of third party components that can be used
Advice is to wrap the ones you use in an abstraction, so the base components can be swapped out if needed without re-writing your app
This also allows setting proper defaults
Render fragments should be used to allow customization within the app without writing duplicate components

## Debugging Server
The server debugging tools are much as you'd expect for c# projects
Can set breakpoints, and exceptions will throw as you'd expect
For errors encountered during server rendering (either SSR, or the pre-render part of interactiveserver), a normal error 500 will be shown
For client-side errors, it will show in the javascript console
Turning on DetailsErrors: true in the app settings will show more detailed errors and stack traces

## Debugging WebAssembly
YOu can debug webassembly by using a debugging browser. Chrome and edge both support this, using Ctrl-Alt-D shortcut
Generally however, Visual studio is better at it

## Hot reload
Hot reload is an option in visual studio. It generally works, but can be buggy at times
Using the command line command "dotnet watch" in the bin directory generally works a bit better

## CSS
The CSS is loaded from the App.Razor component
By default, it uses the bootstrap css components, but this can be changed if needed
CSS is loaded in the order that it is defined. By default, bootstrap comes first, and then the app.css. This means we can override bootstrap if needed

## Isolated CSS
Blazor supports isolated css, which is css applied to only a single component
This is done by creating a file called <ComponentName>.razor.css
Any CSS added to this file will only affect the single component. It won't affect child components either
If you want it to affect child components, the css selected ::deep can be added
Note that this will only work if the component is contained with an html tag. Any tag will do, but it needs something to attach to

## Blazor to Javascript
Blazor can run javascript as long as a couple things are true
Firstly, it can only be run from interactive components, or from the OnInit event. Otherwise the code execution will be server side, and won't be able to be run
Secondly, usually only methods from the window javascript namespace can be called. Other calls will be out of scope
However, this can be changed by importing javascript modules, and using the jsmodule framework to run the script

## Javascript to Blazor
You can call blazor code from Javascript
This generally not something that is regularly done, but it is possible, with some limitations
Didn't bother to learn this at the moment, as it's pretty niche

## bUnit
bUnit is the unit testing framework for blazor. It's open source
bUnit can use razor or c# syntax, and use any of the three major testing frameworks, nUnit, xUnit or MSTest
It can do authentication, mocking, and a bunch of other stuff