20
T HE D EVELOPMENT
V IEWPOINT
Definition
Concerns
Models
Problems and
Pitfalls
Stakeholders
Applicability
Describes the architecture that supports the software development
process
Module organization, common processing, standardization of
design, standardization of testing, instrumentation, and codeline
organization
Module structure models, common design models, and codeline
models
Too much detail, overburdened architectural description, uneven
focus, lack of developer focus, lack of precision, and problems with
the specified environment
Production engineers, software developers and testers
All systems with significant software development involved in their
creation
A considerable amount of planning and design of the software development
environment is often required to support the design, build, and testing of
software for complex systems. Things to think about include code structure
and dependencies, build and configuration management of deliverables,
system-wide design constraints, and system-wide standards to ensure techni-
cal integrity. It is the role of the Development view to address these aspects of
the system development process, as it is this view that addresses the specific
concerns of the software developers and testers.
This viewpoint is relevant to nearly all large information system projects
because almost all of them have some element of software development,
whether it is configuring and scripting off-the-shelf software, writing a
system from scratch, or something between these extremes. The importance of
this view depends on the complexity of the system being built, the expertise of
357
358
 P A R T III  A V I E W P O I N T C A T A L O G
the software developers, the maturity of the technologies used, and the
familiarity that the whole team has with these technologies.
In this view you need to focus on concerns that are architecturally signif-
icant. You should view your work as providing a stable environment for the
more detailed design work that will be performed as part of the software
development activity.
C ONCERNS
Module Organization
The large systems you are likely to encounter as an architect may be built
from hundreds of thousands of lines of source code spread over thousands of
files. Source files are normally organized into larger units called modules that
contain related code (such as the code to implement a library or a functional
element). Arranging code in a logical structure like this helps to manage
dependencies and helps developers to understand it and work on it without
affecting other modules in unexpected ways.
When working with a complex module structure, you need to identify and
thoroughly understand and manage the dependencies between the modules to
avoid ending up with a system that is difficult and error-prone to maintain,
build, and release.
Common Processing
Any large system will benefit from identifying and isolating common process-
ing into separate code modules. For example, standardizing how the system
logs messages and handles configuration parameters can significantly sim-
plify its administration.
The Development view helps ensure that the areas of common processing
are identified and clearly specified. You will typically do this only in outline
form, adding further refinement and detail as development progresses.
Standardization of Design
Most systems are developed by teams of software developers rather than indi-
viduals. Standardizing key aspects of design provides critical benefits to the
maintainability, reliability, and technical cohesion of the system (and saves
time, too). You can achieve design standardization by using design patterns
and off-the-shelf software elements.
C H A P T E R 20  T H E D E V E L O P M E N T V I E W P O I N T
 359
Standardization of Testing
Standardization of test approaches, technologies, and conventions helps
ensure a consistent approach to testing and speeds up the testing process.
Key concerns include test tools and infrastructure, standard test data, stan-
dard test approaches, and test automation.
Instrumentation
Instrumentation is the practice of inserting special code for logging informa-
tion about step execution, system state, resource usage, and so on that is
used to aid monitoring and debugging. Because instrumentation can have an
adverse impact on performance, it should be possible to switch off this capa-
bility, alter the level of detail at which messages are logged, and possibly even
use build tools to remove the instrumentation code altogether.
System messages can be logged to a system console, a file, or a message
service, and metrics on system usage can be logged to a file or a database for
later analysis.
Codeline Organization
The system’s source code needs to be stored in a directory structure, man-
aged via a configuration management system, built and tested regularly
(ideally every time the software changes—“continuous integration”), and
released as tested binaries for further testing and use. The way that all of
this is achieved is normally termed the codeline organization for a system.
The codeline is a particular version of a set of source code files with a well-
defined organizational structure, usually with an associated automated
system to build, test, and release a specified version or variant of the
system.
Ensuring that the system’s code can be managed, built, tested, and
released is crucial to achieving a reliable system—particularly when you’re
using iterative development and many releases are necessary. As an architect,
you may wish to specify, in outline form at least, how this is to be done, or
better still, work with the development team to define the approach and
design its implementation.
Stakeholder Concerns
Typical stakeholder concerns for the Development viewpoint include those
shown in Table 20–1.
360
 P A R T III  A V I E W P O I N T C A T A L O G
TABLE 20–1 STAKEHOLDER C ONCERNS FOR THE DEVELOPMENT VIEWPOINT
Stakeholder Class
 Concerns
Developers
 All concerns
Production engineers
 May be involved in or have responsibility for provisioning development
and test environments, and mechanisms and controls over the system’s
transition into production
Testers
 Common processing, instrumentation, test standardization, and possibly
codeline organization
M ODELS
Module Structure Models
The module structure model defines the organization of the system’s source
code, in terms of the modules into which the individual source files are col-
lected and the dependencies among these modules. It is also common to
impose some degree of higher-level organization on the modules themselves
to avoid having to enumerate many individual dependencies.
Once you have identified a set of modules into which you can organize
the source files, you can use the common architectural approach of grouping
modules at similar abstraction levels into layers. You can then organize these
layers into a dependency stack from the most abstract or highly functional
(conceptually at the top) down to the least (at the bottom). You can then
define interlayer dependency rules to avoid unwanted dependencies between
modules at very different abstraction levels. Typically, software in a module
communicates only with other modules at the same layer or in the layers
directly above and below it (although there are often exceptions to this rule
for performance or efficiency reasons).
In some situations (e.g., when separate module structures are needed for
client and server elements), you may need a number of such models. In other
cases (e.g., when developing an extension to a monolithic application pack-
age), a module structure model is less useful.
NOTATION A module structure model is often represented as a UML compo-
nent diagram, using the package icon to represent a code module and depen-
dency arrows to show intermodule dependencies. If you require higher-level
module organization, you can show module grouping by enclosing packages
annotated with suitable stereotypes.
Another common alternative is a simple boxes-and-lines diagram that
shows the layers, their relative ordering, and the components within them.
C H A P T E R 20  T H E D E V E L O P M E N T V I E W P O I N T
 361
EXAMPLE Figure 20–1 shows an example of using UML to document a
module structure model.
This layer model shows a module organization with three layers, each
layer being represented by a stereotyped package. The system’s modules
are shown as UML packages within the layers.
The model shows that the domain layer depends on the utility layer,
which in turn depends on the platform layer (i.e., the domain-layer com-
ponents can access only the utility-layer components, and so on).
However, you can also see that nonstrict layering has been used in this
system because all of the domain-layer components depend on facilities
provided by the Java Standard Library component rather than accessing its
facilities via intermediate utility components. (In contrast, the domain-level
components cannot access the JDBC Driver component.)
ACTIVITIES
Identify and Classify the Modules. Group the source code for the system
into a set of modules, and (optionally) classify them—by abstraction or other
criteria—into a higher-level organization.
Identify the Module Dependencies. Identify a clear set of dependencies
between the modules (or the higher-level groups) so that everyone involved
in the design and construction of the system can understand the impact of
making changes.
Identify the Layering Rules. If a layered approach is to be used, you need to
design a set of rules to be followed with respect to the layers. Can modules
call modules only in their own layer and the one above or below, or do you
want a less rigid rule in order to meet system quality properties such as per-
formance and flexibility?
Common Design Models
To maximize commonality across element implementations, it is desirable to
define a set of design constraints that apply when designing the system’s soft-
ware elements. Such design constraints are valuable for two principal reasons.
You can reduce risk and duplication of effort by identifying standard
approaches to be used when solving certain types of problems.
Commonality among system elements helps increase the system’s overall
technical coherence and makes it easier to understand, operate, and
maintain.
A common design model has the following three important pieces.
1. A definition of the common processing required across elements, such as:
• Initialization and recovery
• Termination and restart of operation
• Message logging and instrumentation
• Internationalization
• Use of third-party libraries
• Processing configuration parameters (at startup or while running)
• Security (e.g., authentication or encryption)
• Transaction management
• Database interaction
• Internal and external interfacing
These aspects of your software element designs can benefit greatly
from using a standard approach across all system elements. Identifying
C H A P T E R 20  T H E D E V E L O P M E N T V I E W P O I N T
 363
and defining common processing is a key architectural task that directly
contributes to the overall technical coherence of the system.
2. A definition of standard design approaches that should be used when
designing the system’s elements. These start to emerge when (having
defined the functional structure) you think ahead a little about how
the subsystems might be implemented. When you see situations
where the same sort of processing is performed by different elements,
or where you know that the implementation of a certain aspect of an
element will have a system-wide impact, you should consider whether
you need a standard design approach. When identifying such an
approach, you must define what the approach is, where it should be
used, and why it should be used. In other words, it is a special sort of
design pattern.
3. A definition of what common software should be used and how it should
be used. This may be the result of making other higher-level decisions
(e.g., selecting an access library for your chosen database) or identifying
a reusable component (e.g., a third-party message-logging library or a
locally developed graphical user interface element) that can save you
development time and reduce risk. In either case, your common design
model needs to clearly identify what common elements should be used,
where they should be used, and how they should be used.
As with the module structure model, you may need to define different de-
sign constraints for different parts of the system. In any case, as an architect,
you are only starting a task that will continue throughout the design and build.
N OTATION The common design model is a partial design document, and as
such, the notations it uses are those of software design—usually a combina-
tion of text and more formal notation such as UML.
The following example shows some possible design constraints from a
common design model.
EXAMPLE Here is an example of a common design model.
Common Processing Required
1. Message logging
•All components must log human-readable messages that clearly
state what has occurred and any corrective action that is expected in
response.
364
 P A R T III  A V I E W P O I N T C A T A L O G
•••[. . .]
Messages must be logged at one of the following levels: Fatal, Error,
Warning, Information, Debug. Fatal should be used to indicate an
unrecoverable error, where the component will stop immediately;
Error indicates an unrecoverable error, where the component can
reset itself and continue execution; Warning indicates a possible
error or unexpected condition that may need operator intervention to
review and address; Information is used to report conditions that
occur during normal operation and require no operator intervention;
Debug should be used to indicate internal details of the component’s
operation.
Components should log messages at all five possible logging levels.
Logging should be achieved via a standard library (as defined later)
to standardize destination, format, configuration, and so on.
2.
 Internationalization
• All user- and administrator-visible strings must be stored in
message catalogs so that hard-coded strings are not present in
source code.
• Parameters must be inserted into internationalized strings using
position-independent placeholders to avoid problems with ordering
across languages.
• Locale-sensitive information (dates, times, currency strings, and so
on) must be formatted according to the current locale in force, and
default formats should not be used.
• Strings logged at Debug level or for other purely internal use should
not be internationalized but should be hard-coded in the source
code.
[. . .]
Standard Design
1. Internationalization
••For internationalization of locale-sensitive resources (primarily
strings), use an external resource catalog to store resources outside
the source code files. This means that all strings must be extracted
from a message catalog before they can be used in a program
(e.g., to write a log message).
As the server software is being written entirely in Java, the interna-
tionalization implementation will use the Java Platform’s native
internationalization facilities: the resource bundle, the formatting
classes in the java.text package, and the Locale class.
C H A P T E R 20  T H E D E V E L O P M E N T V I E W P O I N T
 365
• The relationships between these different elements of the interna-
tionalization technology are as follows. [. . .]
• [You would place a definition of a design pattern for using the
Java internationalization facilities here.]
[. . .]
Standard Software Components
1. Message logging
• All message logging must be performed using the standard CCJLog
package, which is part of the standard build environment.
• The CCJLog package must be used in a standard way, which is doc-
umented as a code sample in the src/server/sample
/logging/CCJLog source directory.
[. . .]
ACTIVITIES
Identify Common Processing. Identify what common processing is required,
where the processing is required (in all elements or just some?), and how the
common processing should be performed.
Identify the Required Design Constraints. Establish whether any common
processing should be standardized and whether critical aspects of subsystem
design will have a negative system-wide impact if not designed in a certain
way. If you find such situations, consider whether you can impose a design
constraint that will resolve the problem, and, if so, add it to the list.
Identify and Define the Design Patterns. Document a set of mini design
patterns that clearly define the constraints. The constraints are defined in
terms of the software design that needs to be followed, the applicability of the
constraint (i.e., where to use it), and the rationale for the constraint (to allow
those following it to understand its role).
Define the Role of Standard Elements. Consider whether you have any standard
software elements that can be shared among subsystems. You will often identify
such standard elements when considering the system’s common processing. If you
find standard elements, clearly define their roles and how they should be used.
Codeline Models
Although you certainly don’t want to be dictating the minutiae of the software
developers’ lives, you do need to ensure that there is order rather than chaos
when it comes to the organization of the system’s code.
366
 P A R T III  A V I E W P O I N T C A T A L O G
The key things to define are the overall structure of the codeline; how the
code is controlled (usually via configuration management); where different
types of source code live in that structure; how it should be maintained and
extended over time (in particular, how any concurrent development of differ-
ent releases should work); and the automated tools that will be used to build,
test, release, and deploy the software. A codeline model normally needs to
capture the following essential facts:
How code will be organized into source files
How the files will be grouped into modules
What directory structure will be used to hold the files
How the source will be automatically built and tested to form candidate
releasable binaries
What type and scope of tests will need to be run regularly and when they
should be run
How the binaries will be released into a test or production environment
for testing and use, again ideally via an automated process
How the source will be controlled using configuration management
(including any use of branching, change sets, and so on) to coordinate
multiple developers working on it concurrently
What automated tools will be used for the build, test, and release process
and how they will work together in order to form a complete continuous
integration and delivery system
Defining these aspects of the development environment is an important
part of achieving reliable, repeatable build and release processes. The infor-
mation you provide through your model will help prevent confusion and frus-
tration as developers work together.
In situations where development of the system will be distributed among
different teams or among members of teams working at different locations,
addressing this concern becomes even more important. You may have to take
into account factors such as different time zones or even the different lan-
guages spoken by development staff.
Depending on the skill and experience of the developers, you may be
comfortable leaving the majority of this work to your design team; at the
other extreme, you may want to specify this in some detail.
NOTATION In principle, you can represent the codeline model by using struc-
tured notations such as UML. However, our experience of trying this suggests
that it often isn’t worth the bother. A simple approach based on text and tables
with a few clear diagrams to explain the conventions used should suffice.
C HAPTER 20  T HE D EVELOPMENT V IEWPOINT
 367
ACTIVITIES
Design the Source Code Structure. Design the overall structure of the direc-
tory hierarchy to be used to store your system’s source code. This must be
flexible enough to provide easy maintenance but simple enough that develop-
ers know where their source files should live.
Define the Build, Integration, and Test Approach. To achieve a reliable
system build process, you need to mandate a common approach across the
system. A build and release specialist may do this for you, but the approach
used for automating the build, integration, and testing does need careful de-
sign. Whatever approach you use, it must make it possible to easily build the
system automatically and also allow developers to use central or local copies
of the latest build.
Define the Release Process. Having completed a clean build of the system,
you need to release the resulting work products (binaries, libraries, gener-
ated documentation, and so on) for testing and use. To ensure that this pro-
cess is reliable and repeatable, you must design a clear process, again
preferably automated. As before, specialists may do the design for you, or
you may need to do it yourself. It is particularly important to be clear about
the build validation (such as automated test suite execution) that needs
completion before release. This process will need to use any deployment
tools that are required in your environment, whether internal to your orga-
nization or supplied by a third party if you are deploying software to an
external hosting environment, such as externally hosted servers or a public
cloud computing service.
Define the Configuration Management. To ensure repeatability and techni-
cal integrity, you must use a common approach to configuration manage-
ment. Its definition should encompass the tools to be used, the configuration
structures (such as variants, branches, and labels) to be used, and the pro-
cess for managing the deliverables under configuration control.
P ROBLEMS AND P ITFALLS
Too Much Detail
Most software architects are experienced software designers, which means
that you probably have a lot of background knowledge related to the process
of software design and implementation. The danger that stems from this is
the temptation to use the Development view to define low-level details about
the system’s implementation that are really the concern of the designers and
implementers.
368
 P A R T III  A V I E W P O I N T C A T A L O G
RISK REDUCTION
 Minimize the number of design constraints you identify. Identifying too
many is often counterproductive and causes problems as developers try
to shoehorn their elements into the space left by a number of different
constraints (or simply ignore them).
 Carefully review everything you describe in the Development view, and
question whether it is architecturally significant. If not, eliminate that
detail from the Development view.
Overburdened Architectural Description
A problem related to having too much detail is the question of where to put
the contents of the Development view (particularly in the common design
model). For a complex system, the common design model can require a signif-
icant amount of text, and given that it is aimed at a specialized group of
stakeholders, it can seem out of place in the main AD document.
RISK REDUCTION
 Capture the details of the system-wide design constraints in a separate
document specifically aimed at the software developers, and then sum-
marize the constraints required and their rationale in a short section of
the AD. This allows interested stakeholders to satisfy themselves that the
design constraints have been considered, without needing to understand
the details of these constraints.
Uneven Focus
We all have a tendency to focus on things that we understand and find inter-
esting. This can lead to a situation where, for example, the design patterns to
be used for network request handling are discussed in minute detail, but the
initialization processing required of each element is hardly considered at all.
RISK REDUCTION
 Try to step back from the system and consider all of the aspects of soft-
ware development that need to be defined at an architectural level.
 Find specialist expertise to advise you in areas you aren’t familiar with.
Lack of Developer Focus
Always remember that the primary (and often only) customers of the Devel-
opment view are the software developers and testers working on your project.
C H A P T E R 20  T H E D E V E L O P M E N T V I E W P O I N T
 369
The Development view must answer their questions and be relevant to their
concerns. If it isn’t, it will almost certainly be ignored.
RISK REDUCTION
 Involve the developers and testers in defining the Development view.
 Delegate aspects of the view’s development to senior software developers
when possible, to give the software development team ownership of the
aspects of the architecture that affect them.
Lack of Precision
Because the Development view has to cover many aspects of the software
development, and because you are unlikely to have expertise in all of them,
lack of precision is a risk. Developers might misinterpret imprecise descriptions
or, if they cannot understand the descriptions, might ignore them altogether.
RISK REDUCTION
 This problem often occurs when an architect knows that it is important to
define some aspect of the system but knows little about it and thus si mply
states that it needs to be performed. When defining the Development view,
make sure to review its contents early with the software d evelopers and
testers to check that the view’s definitions are precise enough.
 Do not be afraid to make use of the knowledge of subject matter experts
where your experience is limited—you are not expected to be an expert in
everything!
Problems with the Specified Environment
Keeping up-to-date with new and emerging technologies takes a lot of time. It
is particularly hard to get reliable information on how mature those technolo-
gies are and how appropriate they might be for your architecture.
This imposes the risk of specifying aspects of the Development view
based on out-of-date (or perhaps just incorrect) knowledge and assumptions,
which can lead to later problems in development or live operation and damage
your credibility with developers.
The other related mistake that is easy to make is to try to impose ap-
proaches that have worked well before but that are simply wrong for the
project teams in the environment in which you’re currently working. Short-
lived stand-alone systems require a different development environment from
huge long-lived product lines that will be used for many years by external
customers. Make sure you understand the needs and constraints of the
project environment before trying to define the Development view.
370
 P A R T III  A V I E W P O I N T C A T A L O G
RISK REDUCTION
 Make sure you specify technology and techniques you really know about,
or get trusted, expert advice from subject matter experts to help make the
relevant decisions.
Understand what is needed in the current project environment and make
sure that your Development view reflects these needs and doesn’t over-
complicate or oversimplify the development environment.
Delegating the research and design of aspects of the Development view to
members of the software development team can help alleviate this prob-
lem while having other positive side effects, such as giving the software
developers a heightened sense of ownership of the system.
C HECKLIST
Have you defined a clear strategy for organizing the source code modules
in your system?
Have you defined a general set of rules governing the dependencies that
can exist between code modules at different abstraction levels?
Have you identified all of the aspects of element implementation that
need to be standardized across the system?
Have you clearly defined how any standard processing should be
performed?
Have you identified any standard approaches to design that you need all
element designers and implementers to follow? If so, do your software
developers accept and understand these approaches?
Will a clear set of standard third-party software elements be used across
all element implementations? Have you defined the way they should be
used?
Will the development and test environments that have been defined work
reliably and be usable and efficient for developers and testers to work in?
Have you or someone else defined a suitable set of tools to reliably auto-
mate the end-to-end build, integration, test, and release processes? Does
the set of tools include any internal or third-party tools that you require
to deploy to the internal or external test and production environments
that you are using?
Is this view as minimal as possible?
Is the presentation of this view in the AD appropriate?
C H A P T E R 20  T H E D E V E L O P M E N T V I E W P O I N T
 371
FURTHER R EADING
Many books discuss the use of design patterns in software development, the
original book being, of course, Gamma et al. [GAMM95]. This topic is
explored further in Coplien et al. [PLOP05–99, PLOP06].
There are a number of good books covering relevant topics such as con-
figuration management, continuous integration, automated testing, release
processes, and so forth. [AIEL10] is a fairly high-level overview of the entire
area, focusing on configuration management and release control, and
[BERC03] is a very thorough guide to software configuration management,
illustrated using a set of patterns. [DUVA07] is a thorough and practical
guide to continuous integration, and [HUMB10] is a detailed guide to auto-
mating the processes involved in building, testing, and releasing software. Fi-
nally, there are a large number of books on automated testing, but we
particularly like [FREE09], which provides lots of practical advice on automa-
tion but also gets behind the mechanisms to show how and why to put auto-
mated testing at the heart of the development process.