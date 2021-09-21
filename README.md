# Simple Middleware
The repo showcases how to create a simple piece of middleware that adds a header to the response of every request. 

## The Request Pipeline

The request pipeline consists of a sequence of request delegates, called one after the other. The following diagram demonstrates the concept.


![Request Pipeline](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index/_static/request-delegate-pipeline.png?view=aspnetcore-5.0)


Each middleware component in the request pipeline is responsible for invoking the `next` component in the pipeline or short-circuiting the pipeline. 

Each delegate can perform operation before and after the `next` delegate. 


## Middleware order
The order that middleware components are added in the `Startup.Configure` method defines the order in which the middleware components are invoked on requests and the reverse order for the response. 


The order is critical for security, performance, and functionality. For this reason, it is reccommended that middleware should be be invoked in the following order:

![Middleware Order](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index/_static/middleware-pipeline.svg?view=aspnetcore-5.0)

## Writing Custom Middleware
Look throug the commit history of this repo to see how custom middleware can be written. 

First I create in-line middleware where an individual request delegate us soecufued ubkube as an anonynour method in `Startup.Configure`.

Next I take steps to encapsulated the delegate in a class and expose it with an extension method.

Note how the middleware class must include
* A public constructor with a parameter of type RequestDelegate.
* A public method named Invoke or InvokeAsync. This method must:
    * Return a Task.
    * Accept a first parameter of type HttpContext.

Finally, I demonstrate how Middleware components can resolve their dependencies from dependency injection (DI) through constructor parameters.

## Resources
* [Middleware Overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0)
* [Writing Custom Middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-5.0)
