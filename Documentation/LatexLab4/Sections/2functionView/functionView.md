17
T HEF UNCTIONAL V IEWPOINT
Definition
Concerns
Models
Problems and
Pitfalls
Stakeholders
Applicability
Describes the system’s runtime functional elements and their respon-
sibilities, interfaces, and primary interactions
Functional capabilities, external interfaces, internal structure, and
functional design philosophy
Functional structure model
Poorly defined interfaces, poorly understood responsibilities, infra-
structure modeled as functional elements, overloaded view, diagrams
without element definitions, difficulty in reconciling the needs of mul-
tiple stakeholders, wrong level of detail, “God elements,” and too
many dependencies
All stakeholders
All systems
The Functional view of a system defines the architectural elements that deliver
the functions of the system being described. This view documents the system’s
functional structure—including the key functional elements, their responsibili-
ties, the interfaces they expose, and the interactions between them. Taken
together, this demonstrates how the system will perform the functions
required of it.
The Functional view is the cornerstone of most ADs and is often the first
part of the description that stakeholders try to read. (Too often, it is also the
only view of the architecture produced.) It is probably the easiest view for
stakeholders to understand. The Functional view usually drives the definition
of many of the other architectural views (particularly Information, Concurrency,
Development, and Deployment). You will almost always create a Func tional
267
268
C ONCERNS
P A R T III  A V I E W P O I N T C A T A L O G
view and will often spend a lot of time refining the functional structure that it
defines.
A major challenge when defining the Functional view is to include an
appropriate level of detail. Focus on what is architecturally significant—in
other words, what has a visible impact on stakeholders—and leave the rest to
your designers. Avoid documenting physical implementation details such as
servers or infrastructure in your Functional view, as this will overcomplicate
your models and confuse your stakeholders. (You will document these
elements in your Deployment view.)
Functional Capabilities
Functional capabilities define what the system is required to do—and, explic-
itly or implicitly, what it is not required to do (either because this functional-
ity is outside the scope of consideration or because it is provided elsewhere).
On some projects, you will have an agreed-upon set of requirements at
the start of architecture definition, and you can focus in the Functional view
on showing how your architectural elements work together to provide this
functionality. However, in many projects this isn’t the case, and as we dis-
cussed in Chapter 8 and Chapter 16, the onus will be on you in this case to
ensure that there is a clear definition of what the system will (and won’t) be
required to do.
External Interfaces
External interfaces are the data, event, and control flows between your sys-
tem and others.
Data can flow inward (usually resulting in an internal change of system
state) and/or outward (usually as a result of internal changes of system
state). Events can be consumed by your system (notifying your system that
something has occurred) or may be emitted by your system (acting as notifi-
cations for other systems). A control flow may be inbound (a request by an
external system to yours to perform a task) or outbound (a request by your
system to another to perform a task).
Interface definitions need to consider both the interface syntax (the struc-
ture of the data or request) and semantics (its meaning or effect).
Internal Structure
In most cases, you can design a system in a number of different ways to meet
its requirements. It can be built as a single monolithic entity or a collection of
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 269
loosely coupled components; it can be constructed from a number of standard
packages, linked together using commodity middleware, or written from
scratch; or its functional needs can be met by using network-accessible ser-
vices provided by systems external to this one or even to the organization.
Your challenge is to choose among these many options in order to create an
architecture that meets the requirements, exhibits the required quality proper-
ties, and is fit for purpose.
The internal structure of the system is defined by its internal elements, what
they do (i.e., how they map onto the requirements), and how they interact with
each other. This internal organization can have a big impact on the system’s
quality properties, such as its availability, resilience, ability to scale, and security
(e.g., a complex system that crosses organizational boundaries is generally
harder to secure than a simple one running on a couple of collocated machines).
Functional Design Philosophy
Many of your stakeholders will be interested only in what the system does and
the interfaces it presents to users and to other systems. However, some stake-
holders will be interested in how well the architecture adheres to established
principles of sound design. Technical stakeholders, in particular the de velopment
and test teams, want a sound architecture, because a well-designed system is
easier to build, test, operate, and enhance. Other stakeholders—particularly
acquirers—implicitly want a well-designed system because it is faster, cheaper,
and easier to get such a system into production.
The design philosophy will be underpinned by a number of design char-
acteristics such as the examples listed in Table 17–1.
TABLE 17–1 DESIGN C HARACTERISTICS
Design
Characteristic
Coherence
Cohesion
Consistency
Description
Does the architecture have a
logical structure, with the
elements working together
to form a whole?
To what extent are the functions
provided by an element strongly
related to each other?
Are mechanisms and design
decisions applied consistently
throughout the architecture?
Significance
If the architecture doesn’t look coherent, this
may indicate that the element decomposition
is wrong, and it may make it hard for stake-
holders to understand.
In a highly cohesive system, related func-
tions are grouped together, resulting in sim-
pler, less error-prone designs.
A consistently designed and implemented
system is much easier to build, test, operate,
and evolve than one with a lot of accidental
inconsistency.
Continued on next page
270
 P A R T III  A V I E W P O I N T C A T A L O G
TABLE 17–1 DESIGNDesign
Characteristic
Coupling
Extensibility
Functional
flexibility
Generality
Interdepen-
dency
Separationconcerns
Simplicity
of
C HARACTERISTICS (C ONTINUED )
Description
 Significance
How strong are the element
interrelationships To what extent
do changes in one module affect
others?
Loosely coupled systems are often easier to
build, support, and enhance but may suffer
from poor efficiency compared with a mono-
lithic approach.
Will the architecture be easy to
extend to allow the system to per-form new functions in the future?Extensibility is often the result of other proper-
ties such as coherence, low coupling, simplicity,
and consistency, but it is worth bearing in
mind explicitly when considering your designs.
How amenable is the system to
supporting changes to the
functions already provided?
Systems that are designed to be easy to
change are usually harder to build and typi-
cally are less efficient than systems that are
less adaptable.
Are the mechanisms and decisions If the solutions embodied in the architecture
in the architecture as general as is are generic, the architecture will be amenable
practicable?
 to extension and change. However, this must
be balanced against any resulting increase in
cost and complexity.
What proportion of processing
steps involves interactions
between elements as opposed to
within an element?
Communicating between certain types of ele-
ments can be an order of magnitude more
expensive (in terms of processing time and
elapsed time), and significantly less reliable,
than performing an operation within a func-
tional element.
To what extent is each internal
 High separation results in a system that is
element responsible for a distinct easier to build, support, and enhance but
part of the system’s operation? To may adversely impact performance and scal-
what extent is common processing ability compared with a monolithic approach.
performed in only one place?
Are the design solutions used
within the system the simplest
ones that would be suitable?
Complexity makes systems difficult and
expensive to build, comprehend, operate, and
evolve, but a simplistic approach may well not
meet the requirements of a sophisticated sys-
tem.
In general, these design characteristics have a positive effect on a number of
system qualities, particularly those relating to evolution, such as flexibility and
maintainability. They also usually have a positive effect on other system qualities
such as performance and security (e.g., separation of concerns and simplicity can
make security easier to achieve, while consistency is likely to make performance
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 271
M ODELS
TABLE 17–2 S TAKEHOLDERStakeholder Class
Acquirers
Assessors
Communicators
Developers
System administrators
Testers
Users
CONCERNS FOR THE FUNCTIONAL V IEWPOINT
Concerns
Primarily functional capabilities and external interfaces
All concerns
Potentially all concerns, to some extent, depending on context
Primarily design quality and internal structure, but also
functional capabilities and external interfaces
Primarily functional design philosophy, external interfaces,
and possibly internal structure
Primarily design quality and internal structure, but also
functional capabilities and external interfaces
Primarily functional capabilities and external interfaces
and scalability easier to achieve). In some cases, though, you need to co nsider
the possibility of a negative relationship between “good” design and other system
qualities (e.g., very loosely coupled systems can be less performant than more
tightly coupled ones); in some cases this can mean the need to compromise over
the design characteristics that can be achieved (we note the need for occasional
design compromises in some of the perspectives in Part IV).
Principles and patterns are good techniques for defining how you want the
design of the system to embody these design characteristics, as they can guide
the system’s designers to make design decisions that support the characteristics
that you are most interested in achieving. We discuss this further in Chapter 8.
Stakeholder Concerns
Typical stakeholder concerns for the Functional viewpoint include those listed
in Table 17–2.
Functional Structure Model
The functional structure model typically contains the following elements.
Functional elements: A functional element is a well-defined runtime
(as opposed to design-time) part of the system that has particular
responsibilities and exposes well-defined interfaces that allow it to be
connected to other elements. At its simplest level, an element is a
272
 P A R T III  A V I E W P O I N T C A T A L O G
software code module, but in other contexts it could be an application
package, a data store, or even a complete system.
Interfaces: An interface is a well-defined mechanism by which the func-
tions of an element can be accessed by other elements. An interface is
defined by the inputs, outputs, and semantics of each operation offered
and the nature of the interaction needed to invoke the operation. Common
types of interfaces found in information systems are remote procedure calls
(RPCs) of various types, messaging, events, and in some cases interrupts.
Connectors: Connectors are the pieces of your architecture that link the
elements together to allow them to interact. A connector defines the
interaction between the elements that use it and allows the nature of the
interaction to be considered separately from the semantics of the opera-
tion being invoked. The nature of the interactions between elements can
be intimately bound up in how they are connected.
The amount of consideration you need to give connectors depends on
your circumstances. At one extreme—for example, when one element
calls another via a simple procedure call—you can just note that one ele-
ment connects to another. At the other extreme, such as a message-
based interface, a connector can be defined as a type of element in its
own right as it provides capabilities to the interactions that occur across
it. As always, the focus needs to be on what is architecturally significant
in the context in which you are working.
External entities: As we defined in Chapter 16, external entities are other
systems, software programs, hardware devices, or any other entity with
which your system interacts. They are obtained from your system’s Con-
text view, and each appears in the functional model at the far end of an
interface, external to your system.
The functional structure model does not define how code is packaged and
executed in processes and on threads, so this view doesn’t constrain element
packaging or deployment—this is the domain of the Concurrency and Deploy-
ment views.
Similarly, it is generally not a good idea to model underlying infrastruc-
ture as functional elements, unless that infrastructure performs a functionally
significant task, independent of the other functional elements, without which
the view doesn’t make sense. Infrastructure that simply supports the opera-
tion of the functional elements should normally not be shown in the Func-
tional view; it is best considered in the Deployment view.
For example, you might well want to show message queues, as they are
important interelement connectors and so the view doesn’t make sense with-
out them, but you probably don’t need to show the message broker that pro-
vides the queues, which doesn’t add anything in this context. The message
broker would be shown in the Deployment view.
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 273
N OTATION You can use a number of techniques to represent the Functional
view in a model.
UML component diagrams: Using UML for a Functional view has a num-
ber of advantages, including its widespread comprehension and its flexi-
bility. The main UML diagram you will use for the Functional view is a
component diagram, which shows a system’s elements, interfaces, and
interelement connections.
EXAMPLE Figure 17–1 shows the typical elements in a UML component
diagram. The system consists of two internal elements, Variable Capture
and Alarm Initiator, interacting with one external element, Temperature
Monitor. Variable Capture exposes one interface, VariableReporting,
which is invoked by Temperature Monitor, and Alarm Initiator exposes
one interface, LimitCondition, which is invoked by Variable Capture.
VariableReporting is tagged with information that tells us it is an XML
remote procedure call, over the HTTP protocol, and that, at most, 10
concurrent invocations can exist at one time.
You represent each of the system’s elements and external entities
with a UML component icon, annotated with its name and any stereo-
type needed to make the nature of the element clear. (Stereotypes allow
you to extend the semantics of standard UML in a logical and consistent
way to meet your individual circumstances.) One particularly useful ste-
reotype is <<external>>, which indicates that the icon refers to an exter-
nal entity, rather than a system element. Another is <<infrastructure>>,
which indicates an infrastructure element of the system that has a
distinct functional role.
UML interface icons attached to a system element represent the inter-
faces it exposes. We have found that the small “lollipop” interface icon is
more effective in the Functional view than the larger stereotyped class
icon. In order to differentiate between types of interfaces, stereotypes
may be defined with associated sets of tagged values that allow the char-
acteristics of particular interfaces to be captured (such as “transport”).
Using tagged values to capture the type of interface, the protocol used to
access it (if any), and the number of concurrent users or connections al-
lowed provides a good basis for interface classification.
Once you have identified elements and interfaces, you can show the
connectors between the interfaces with UML dependencies and informa-
tion flows, as described in the following example.
EXAMPLE The UML component diagram shown in Figure 17–2 is an
example of using UML to document the functional structure of a simple
system. The system under consideration provides a Web storefront
(called the Web Shop) for customers to use when purchasing items from
an online catalog that fits into an existing enterprise software environ-
ment. (To save space, we have omitted the detailed descriptions of the
system components and their interfaces, but obviously these would be
crucial information for a real model.)
The model shows that the system communicates with four external
entities: the Web browsers of the three main user types (customers, cus-
tomer care representatives, and catalog administrators) and an external
system (the order fulfillment system). Our system is composed of five
main functional components linked via a number of connector types
(including HTML over HTTP and publish/subscribe messaging, with an
LU 6.2 external interface).
Customers order from the Web Shop, which interacts with the Product
Catalog, the Order Processor, and the Customer Information System. The
catalog administrators maintain the product catalog via their Web-based
interface, and the customer care representatives maintain the customer
information via a dedicated interface client program (the Customer Care
Interface). When the stock level of a particular item in the catalog is
needed, the Product Catalog accesses this information from the Stock
Inventory (which already exists).
We also have some insights into the nature of the intercomponent
interactions. We know that up to 1,000 customers, 80 customer care
representatives, and 15 catalog administrators may access the system
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 275
simultaneously. We also note that the interaction between the Product
Catalog and the Stock Inventory components takes place using a specific
protocol (presumably due to preexisting technology). We can assume for
this example that the unadorned intercomponent communication takes
place via some form of standard remote procedure call (which we will
assume has been clearly defined elsewhere).
Having said this, one of the interesting points to note about this
model is how much is not obvious from the diagram. The responsibili-
ties of the components aren’t clear, the details of their interfaces aren’t
clear, and the details of how the components interact aren’t clear. This
impresses on us the need to complete the textual descriptions that
underpin the diagram and the need to understand the system via a
number of models rather than just one (e.g., intercomponent interac-
tions can be shown via system scenario modeling, as we described in
Chapter 10).
Other formal design notations: UML is not the only well-defined design
notation suitable for software development. A number of older structured
notations (such as Yourdon, Jackson System Development, and the
Object Modeling Technique of James Rumbaugh) have been successfully
applied to software development problems for many years. The problem
with using any of the notations developed for software design is that
they tend to be fairly weak at describing the concepts (such as large-
scale elements, interfaces, deployment options, and so on) that are
important to architects. The older methods also aren’t widely taught or
used today, and so tool support may be difficult to come by, and they
lack the general familiarity of UML for most people.
Architecture description languages (ADLs): Languages that do directly
support the concepts that software architects are concerned with are gen-
erally known as ADLs. A large number of ADLs have been created
(including Unicon, Wright, xADL, Darwin, C2, and AADL). The great
attraction of ADLs is that they provide native support for some of the
things that we need to capture and reason about in our architectural
designs (such as components and connectors). However, nearly all ADLs
have been developed in the research environment and tend to suffer
from a number of practical drawbacks, including lack of stakeholder fa-
miliarity with them, relatively narrow scope (often only allowing “com-
ponents” and “connectors” to be represented), and an inevitable lack of
mature tool support. For these reasons, despite a number of years of
searching, we still haven’t found an ADL that we’ve been happy to adopt
on a day-to-day basis.
Boxes-and-lines diagrams: Many architects use a functional structure
diagram drawn by using a custom boxes-and-lines notation. Such a dia-
gram should show just the functional elements and their interfaces and
should link the elements to the interfaces they use with a clear graphical
device (typically an arrow, possibly with some annotation) that indicates
the use of a connector. As with any custom notation, be sure to define
the meaning of the notation clearly to avoid confusion.
EXAMPLE The boxes-and-lines diagram shown in Figure 17–3 gives an
alternative, less formal, and possibly more user-friendly representation
of the system described in the previous example.
In this model, we have defined our own notation. Functional elements
are represented by rectangles and the links between them by lines, with
arrows indicating the direction(s) of information flow. External user-
facing interfaces are represented by an icon meant to look like a computer
monitor, and external back-end systems are represented by rectangles
with rounded corners. Data stores are represented by an icon that looks
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 277
like a disk drum, and functional interfaces (the Internet, the message
bus) are represented by a cloud icon. The scope of the system is those
elements within the dotted rectangle.
The benefit of the boxes-and-lines diagram is that nontechnical
stakeholders, particularly business users and sponsors, may find it eas-
ier to understand. Such a model can be an invaluable tool in selling the
features and benefits of the system to these stakeholders without getting
bogged down in technical detail. Often you may use the boxes-and-lines
diagram as a front for more detailed, rigorous UML models.
Although the boxes-and-lines diagram can be used less formally
than a UML model, you shouldn’t use this as an excuse for being less
rigorous. In particular, early in architecture definition, you should define
a standard notation for your diagrams—and make sure you stick to it.
Try to develop icons that give an indication of the underlying purpose of
the elements modeled (e.g., the disk-drum icon shown in Figure 17–3 is
often used to model data stores).
You should always support any such model with a definition of its
elements and the interfaces between them, presented in a standardized way.
Sketches: You can create a less formal feel for the view by using a sketch,
that is, by introducing an ad hoc notation as required to represent each of
the aspects of the view that are significant for your system. The use of a
sketch is often required to effectively communicate essential aspects of
the view to nontechnical stakeholders. The problem with this approach is
that it can lead to a poorly defined view and confusion among stakehold-
ers. As with the boxes-and-lines diagram, you can get around this by
using a sketch to augment a more formal view notation (such as UML)
and using different notations for different stakeholder groups.
Representing procedure-oriented element interactions is relatively
straightforward, but modeling message-oriented interactions (such as those
found where elements are connected via publish/subscribe messaging sys-
tems) can be significantly harder.
We used to model message-oriented interfaces by showing the message
distribution mechanism (typically a piece of message-oriented middleware)
as a functional element and connecting the various message source and
destination elements to it. This does get the point across, but it’s difficult to
discern the overall message flow in the system. A better approach, origi-
nally suggested by Garland and Anthony [GARL03], is to use ports and
information flows to model message-oriented interactions between system
elements.
The notion of ports comes originally from the real-time systems community,
where a port is an abstract representation of the source or destination of mes-
sages. A more general notion of ports was integrated into version 2 of UML, and
one of their uses can be to clearly show the messaging within a system.
EXAMPLE An example of using ports and information flows for mes-
saging is shown in the UML model in Figure 17–4.
This diagram illustrates part of a notional system in a financial insti-
tution where prices are calculated by one system element (the Price Cal-
culator) and distributed to the other system elements via asynchronous
messages. The small boxes attached to the system elements represent
ports. The one attached to the Price Calculator is an output port (it cre-
ates messages), and the ones attached to the other elements are input
ports (they receive messages). A UML 2 information flow connector is
used to indicate the message flow between elements, with a stereotype
to indicate the type of messaging in use and the “information conveyed”
annotation capturing the message type (publish/subscribe messaging
and “Prices” in the example).
When the message-oriented interactions are illustrated by using a sepa-
rate notation, they can be combined with procedure-oriented element interac-
tions on a single diagram without fear of confusion. You can also use such a
technique to model higher-level messaging systems, such as those that imple-
ment EAI architectures.
Remember that as we said earlier, a Functional view should describe only
the system’s functional elements. If you need notational items to represent
deployment, concurrency, or other aspects of the system, your Functional
view has become overloaded.
Note: When talking about system design notations, it’s also worth men-
tioning the existence of SysML, a design language for systems engineering,
which is based on UML 2 (SysML is actually defined as a UML 2 profile).
We’ve been following the development of SysML over a number of years, and
while it’s undoubtedly a useful tool for people working in systems engineer-
ing, we haven’t found it to be a better alternative to UML 2 for information
systems design. SysML is aimed at situations where systems engineers need
to integrate hardware, software, personnel, facilities, and other varied aspects
of very large systems, rather than the more focused problem of the design of
an information system. The sysml.org, omgsysml.org, and sysmlforum.com
Web sites are good places to find out more about SysML and to track its
evolution.
ACTIVITIES
Identify the Elements. You can identify the functional elements by fol lowing
these steps.
1. Work through the functional requirements, deriving key system-level
responsibilities.
2. Identify the functional elements that will perform those responsibilities.
3. Assess the identified set against the desirable design criteria.
4. Iterate back to refine the functional structure until you judge it to be
sound.
Of course, some elements may be defined for you already (e.g., software
libraries, software packages, preexisting systems or subsystems), in which
case the process for these elements is one of understanding rather than iden-
tifying and designing.
Refining the set of functional elements involves applying one or more
refinements to the functional structure.
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 281
Generalization: identifying some common responsibilities across a number
of elements and introducing a number of more general elements that can
be reused across the system to perform these tasks. Generalization is par-
ticularly important as part of a larger enterprise or product-line architec-
ture to allow reuse of software assets across a number of similar products
or systems.
Decomposition: breaking a large, complex element into a number of
smaller subelements. For large systems, you will often need to break the
top-level functional elements into more manageable subsystem-level ele-
ments to allow them to be designed and built.
Amalgamation: replacing a number of small functional elements with a
larger element that includes all of the functions of the smaller ones.
Amalgamation is typically used when a large number of small but similar
functional elements have been identified. In such cases, it often makes
sense from an architectural perspective to replace the smaller elements
with a single large element that can factor out the commonality between
the smaller ones and reduce the amount of interactions the system
requires.
Replication: replicating either a system element or a piece of processing.
An example is data validation, where you identify a validation element
for incoming data and then replicate it across a number of the system’s
external interfaces. Replication can bring performance benefits, but care
must be taken to keep the replicated components consistent.
If you are using an architectural style to guide your design process, the
process is slightly different because it will involve creating an instantiation of
the style such that the system-level responsibilities are assigned to elements
of the style. This activity is closely related to the next step—assigning respon-
sibilities to the elements.
We don’t talk about the element identification process in a lot of detail in
this book because there are many ways to do it, and the correct method to use
depends on the type of system and the software development approach you
are using. (Procedural, object-oriented, and component-based approaches all
influence component identification in different ways.) See the Further Read-
ing section at the end of this chapter for some sources that discuss element
identification.
Assign Responsibilities to the Elements. Once you have identified candi-
date elements, your next activity is to assign clear responsibilities to them—
that is, the information managed by the element, the services it offers to other
parts of the system, and the activities it initiates. You may have done this in
the previous step; if not, complete it here.
282
 P A R T III  A V I E W P O I N T C A T A L O G
EXAMPLE Table 17–3 shows the responsibilities assigned to two of the
elements for the e-commerce system described in earlier examples.
TABLE 17–3 E XAMPLES OF ELEMENT R ESPONSIBILITIES
Element Class
 Responsibilities
Web Shop
• Present customers with an HTML-based user interface they can access
with a Web browser.
• Manage all state related to the customer interface session.
• Interact with other parts of the system to allow customers to view the cat-
alog and stock levels, buy goods, and view their customer information.
Customer Information •System
 •••Manage all persistent information about customers of the system.
Provide a query-only interface that can be used to retrieve information
held on a particular customer that should be visible to that customer.
Provide an information management programmatic interface that can
be used to create customer information management applications.
Provide an event-driven message-handling interface to accept details of
orders placed by customers and the state changes of those orders.
Design the Interfaces. The services offered by your elements need to be
accessed via well-defined interfaces. The definition of an interface must
include the operations that the interface offers; the input, outputs, precondi-
tions, and effects of each operation; and the nature of the interface (messag-
ing, remote procedure call, Web service, and so on).
A good approach to consider when developing element interfaces is
Design by Contract, an interface design method originally created by Bertrand
Meyer for developing interfaces in object-oriented systems. This approach
involves defining interfaces via “contracts” that use preconditions, postcondi-
tions, and invariants to precisely define operation behavior and relationships.
The appropriate notation for interface definition depends on the type of
interface and who needs to understand this information (considering factors
such as the likely implementation technology, the background of the develop-
ment team, and the kinds of interfaces that need to be described). The follow-
ing are some common interface definition notations.
Programming languages: Interfaces can be defined directly by using a
programming language to define the operation signatures along with text
and/or language assertions to define the operation semantics. This
approach is simple but ties you to the style, assumptions, and limitations
of the particular programming language. This may not be ideal, particularly
if you’re using multiple technologies. This approach works particularly well
for programming libraries or in other situations where the system is really
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 283
a single, large programming artifact or where a single programming lan-
guage is used to implement the entire system.
Interface definition languages (IDLs): Specialist IDLs have been devel-
oped to support mixed-language distributed systems technology (so
there is an IDL for CORBA, an IDL for .NET, WSDL for Web services, and
so on). These languages are independent of implementation technology
and tend to offer simpler facilities than programming languages do, more
suitable for defining architectural interfaces. Provided that your inter-
ested stakeholders can read (or be taught to read) them, these languages
offer a good option for defining operation signatures.
Data-oriented approaches: Interfaces can also be described purely in
terms of messages that are exchanged. Examples of this type of interface
definition include interfaces accessed via messaging systems and inter-
faces defined in terms of structured document exchange (e.g., document-
oriented, Web-service-based interfaces with messages defined using
XML Schema). This approach works particularly well for event-based
interfaces that are defined in terms of the exchange of business events
rather than the invocation of operations.
Whatever notation you use to describe interfaces, remember that an interface
is significantly more than just a simple definition of how you call the
operations. Unfortunately, none of the approaches we have described offer
facilities for defining interface semantics, and so a clear definition of an inter-
face will involve the use of natural language or specialist languages like Object
Constraint Language (OCL) to achieve this. An interface definition must accu-
rately communicate the pre- and postconditions of each operation and how the
operations should be combined in order to perform a useful function (preferably
with examples). Anything less than this is likely to cause significant problems
when the interfaces come to be used.
Design the Connectors. The elements of your system need to communi-
cate in order to achieve the system’s goals, and as you identified your ele-
ment responsibilities, you probably noted the need for elements to interact in
order to implement their responsibilities. The interactions take place across
connectors of some sort that link delegating elements to the interfaces
offered by the elements to which they wish to delegate. Sometimes the type
of connector required is self-evident (such as a simple procedure call),
whereas in other cases you’ll need to think carefully about whether you need
synchronous or asynchronous communication, the resiliency required of the
connector, the acceptable latency of interactions across it, and so on. For
each required interelement communication path in your architecture, add a
connector to the model to support it (be that RPC, messaging, file transfer, or
other mechanisms).
284
 P A R T III  A V I E W P O I N T C A T A L O G
Check the Functional Traceability. The requirements documentation for
your system will have defined a number of functions that the system has to of-
fer. You should carry out a traceability check to ensure that all functional
requirements have been met by the proposed functional structure. Such an anal-
ysis often reveals missing or incomplete functions in the functional structure
model. If it needs to be captured formally, the traceability analysis is usually pre-
sented as a table of functional requirements cross-referenced against the func-
tional model elements with responsibilities relating to those requirements.
Walk through Common Scenarios. It can be extremely valuable and illu-
minating to walk through common system usage scenarios with your stake-
holders, using the Functional view to illustrate how the system will behave in
each case; doing this with the testers, the development team, and the system
administrators can be particularly useful. In such a walkthrough, you should
explain how the system’s elements would interact in order to implement the
scenario. Often, architectural weaknesses or misunderstandings as well as
missing elements are identified as part of such a process. Such a walkthrough
can form part of a larger architectural assessment exercise such as that intro-
duced in Chapter 14.
Analyze the Interactions. Given the impact that excessive interelement
interactions can have, it is useful to analyze the chosen structure from the
point of view of the number of interelement interactions taken during com-
mon processing scenarios. Refining the functional structure to reduce inter-
element interactions to a minimum set without distorting the coherence of the
functional components usually results in a well-structured system with cohe-
sive, loosely coupled elements. It is typically an important step toward an effi-
cient and reliable system. When performing interaction analysis, you need to
make tradeoffs to ensure that reducing interelement interactions does not
result in a distorted system structure with undesirable redundancy or inap-
propriate element partitioning.
Analyze for Flexibility. Successful systems are always under pressure to
change. Given this reality, you should consider how flexible your architecture
is in the face of change, as early in the project as you can. The functional
structure of a system is often one of the primary factors affecting the flexibil-
ity of information systems. It’s useful to work through some “what if” scenar-
ios that reveal the impact of possible future changes on your system. A common
problem at this point is that the changes implied by the change analysis conflict
with those suggested by the interaction analysis. Therefore, it is important that
you trade off these two factors during architectural evaluation in order to find the
right balance for your system, and that you avoid burdening your design with
complexity that will never be used. Again, assessing this can be part of your
architectural evaluation activities; we talk more about this aspect of design in
Chapter 28.
P ROBLEMSC H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 285
AND P ITFALLS
Poorly Defined Interfaces
Many architects define their elements, responsibilities, and interelement rela-
tionships well, yet totally neglect their connectors and interface definitions. De-
fining interelement interfaces clearly can often be something of a chore.
However, it is one of the most important tasks you can perform for the system.
Without good interface definitions, major misunderstandings will occur between
subsystem development teams, leading to a range of problems from build errors
to obviously incorrect behavior to subtle, occasional system unreliability.
RISK REDUCTION
 Define your interfaces and interelement connectors clearly and as early
as possible.
 Review interfaces and connectors frequently to ensure that they are
clearly understood.
 Do not consider element definition complete until interfaces have been
designed.
 Make sure that interface definitions include the operations, their seman-
tics, and examples where possible.
Poorly Understood Responsibilities
It is easy to become very focused on a couple of key scenarios and to consider
the functional elements only in this context. If you don’t define all of the
responsibilities of the elements (and don’t perform traceability analysis), a lot
of confusion can remain over exactly what each functional element is meant
to do. This often leads to problems later: Either functionality is missing
because it fell between the gaps, or functionality is duplicated because two
subsystem development teams both thought that a piece of functionality was
their responsibility.
RISK REDUCTION
 Ensure that element responsibilities are formally defined as early as possible.
 Do not allow the development process to drift into element design without el-
ement responsibilities being formally defined and agreed upon.
 Make sure that all implementers understand where their boundaries are
(and why they are there).
 Make sure that all requirements have been mapped to the elements that
implement them.
286
 P A R T III  A V I E W P O I N T C A T A L O G
Infrastructure Modeled as Functional Elements
In general, you should not model underlying infrastructure as functional ele-
ments. Adding infrastructure elements to the Functional view simply makes it
more confusing without adding useful information. Infrastructure can nor-
mally be hidden inside the functional elements; the Deployment view defines
the infrastructure in more detail. Include infrastructure elements only if their
role is important to understanding how the Functional view works (e.g., you
might want to include a messaging gateway that performs some functional
processing for you, but it’s very rarely the case that including the application
server you are using adds anything).
RISK REDUCTION
 Avoid modeling underlying infrastructure elements as you develop your
initial element model. Focus on functional elements that solve part of the
problem the system is going to address.
 Question the need for any elements that do not have names related to the
domain of the problem being addressed.
 Address specific infrastructure concerns in another view (typically, a
Deployment view).
Overloaded View
The Functional view is the cornerstone of the AD and is often the primary
structuring device. However, beware of letting it become all of the views
rather than just the central view. It is often tempting to overload the Func-
tional view with the intent to make things clearer by adding deployment or
concurrency information or other aspects of the architecture to this view. If
you decide to use a compound view, make this an explicit decision. Don’t al-
low the Functional view to simply creep into being an overloaded description
of many aspects of the system. Such a description is very unlikely to be easy
to understand and therefore is of limited use.
EXAMPLE Figure 17–5 shows an example of what we mean by view
overloading.
This model has a number of problems (even assuming that good
textual descriptions are used to back up the diagram to form a complete
model). It’s obviously related to UML 2, but various bits and pieces of ad
hoc notation have been added: the dashed line from the Socket Library
box to the Web Server box, the dashed lines within the Server Node(s)
box, and so on. This means that we don’t really know what the diagram
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 287
means and will have to ask the architect who drew it. We can probably dis-
cern enough for ourselves to continue, although some problems r emain.
The system provides a salesperson with an interface to allow
something (perhaps a holiday or flight) to be booked.
A number of server-side components (presumably Enterprise Java
Beans, given the name used) implement something on a server
computer. However, we don’t know what components exist, just
that (presumably) there is a group of them.
The server components appear to be implemented by using a utility
library that in turn uses a calendar library (presumably for special-
ist calendar processing for dates). This implies that a layered
model is planned for the component design.
A number of processes run on the server computers: one for the
Web server, one for the application server, and one for the Oracle
database management server. (We’re interpreting the dashed lines
as operating system processes.)
We can discern this sort of information from the model (and presum-
ably could untangle the notation if we could talk with the architect); the
real problem is the overloading of the diagram. Even in our initial un-
derstanding of it, we need to consider functional structure, deployment
across machines, concurrency, software design constraints, and so on.
These are separate concerns, at different abstraction levels, of interest
to different stakeholders. The result is that none of the concerns are
addressed very clearly, and this model probably can’t be used with any
of our stakeholders apart from developers and testers (and even they
will probably need more detail about each of their concerns).
The overloading of the model is probably also one of the reasons that
the notation is confusing. It is very hard to overload a diagram’s func-
tion and not end up with notational confusion because of the need to
represent a number of unrelated concepts together on one diagram.
RISK REDUCTION
 Remove everything from your Functional view except for items related to
the functional elements and their interfaces and connectors.
 Create other views, based on the other viewpoints we define in this book,
to describe the other aspects of your architecture.
 Develop the other views in parallel and cross-reference between views to
illustrate other aspects of the architecture. (We talk about this in
Chapter 23.)
Diagrams without Element Definitions
When developing models that are inherently structural in nature (such as the
functional structure model), there is a tendency to draw the diagram repre-
senting the model’s structure and then to move on to something else without
really defining the entities shown in the model. Defining each of the model
elements carefully can be a tedious process, but unless this is done well, the
model is meaningless.
RISK REDUCTION
 Define each element as it is added to the model, and review the defini-
tions with your stakeholders to check that the definitions are clear and
accurate.
 Do not consider the model complete until every element has a good definition.
Difficulty in Reconciling the Needs of Multiple
Stakeholders
The central role of the Functional view means that most stakeholders are
interested in it. This can cause you significant problems when formulating
C H A P T E R 17  T H E F U N C T I O N A L V I E W P O I N T
 289
the view—how do you create a view description that means something to all
of these different types of stakeholders? End users, developers, system
administrators, and all of the other groups have specific interests and needs,
and you often need to communicate with each in a different way. It is often
difficult to identify a single model or notation suitable for use with all of
these parties.
RISKREDUCTION
Use different modeling languages with different stakeholders. In gen-
eral, stakeholders break into two major groups—technical stakeholders
and nontechnical (business) stakeholders.
You can communicate effectively with the technical stakeholders by
using your primary architectural models (such as the functional structure
model). Some explanation of notation may be required, but on the whole
a technical stakeholder will understand these models.
The nontechnical stakeholders are unlikely to understand your primary ar-
chitectural models, so you’ll need to create simplified models for them,
derived from the primary models. We have found that a less technical
notation (such as the sketches we described in Chapter 12) with brief
textual annotation is often a more effective communication medium here.
Wrong Level of Detail
A common question when creating the Functional view is when to stop. If the
process of functional analysis becomes too detailed and ends up defining too
many layers of elements, you are starting to design all of the software, rather
than just the architecturally significant parts. This can cause real problems,
not least of which is the lack of input from the development team. Conversely,
if you don’t include enough detail, there is a risk that people will misinterpret
your ideas and the system won’t be able to deliver the qualities that you need
it to. Obviously, there is no simple solution to this problem—it depends on the
context.
RISK REDUCTION
 Our experience suggests that if you have to define more than two or
three levels of elements, assuming a limit of about eight to ten functional
elements at the top level, you may have a problem. So, if possible, keep
your level of detail below this limit.
Another danger sign can be the inclusion in the Functional view’s models
of details about the workings or internal structure of functional elements.
If your system is very large, modeling it as a group of systems rather than
working down into the elements would make the problem tractable.
290
 P A R T III  A V I E W P O I N T C A T A L O G
“God Elements”
Software designers often see object-oriented designs that have a single huge
object in the center, with lots of small objects attached to it. This situation is often
dubbed the “God object” problem. The underlying problem in such cases is usu-
ally an inappropriate partitioning of responsibilities among design elements—the
large object (often called “Manager”) is really the entire program, and the small
objects are often just data structures that this object uses. A very similar problem
can exist in ADs, particularly if you consolidate too zealously (perhaps as a result
of interaction analysis).
This problem leads to a situation where the system is hard to maintain
because the God element is terribly complex and difficult to understand. It
also results in this one component’s characteristics dominating the quality
properties that the system exhibits. It becomes difficult to solve related prob-
lems like performance, reliability, or scalability because they all involve
changing this one system element.
EXAMPLE The UML element diagram in Figure 17–6 illustrates the sort
of structure that often suggests the presence of a God element in your
system.
In this situation, the Customer Management system element appears
to exhibit the major characteristic of a God element, namely, nearly all
interelement interactions involve it. From this structure, it is likely that
the Customer Management element contains too much of the system’s
functionality and has dependencies with too many of the system’s ele-
ments. Repartitioning the system into a set of elements with more
evenly distributed functionality would make sense.
RISK REDUCTION
 Aim for a broadly even distribution of system-level responsibilities
among your major elements. As a guideline, if you find more than 50%
of your system’s responsibilities concentrated in less than 25% of your
functional elements, you may be heading toward a number of large ele-
ments and your system will lack cohesion, be difficult to develop, and be
resistant to change.
Too Many Dependencies
The converse to the God object problem is static object diagrams that look like a
number of spiders fighting for control. Complex interactions between el ements
make the system harder to design and build and may lead to a solution that is
hard to change and performs poorly.
RISK REDUCTION
 This problem can often be the symptom of too many small elements in
the system; practicing some judicious compression may help you resolve
it.
 In general, a system element should need to be aware of the existence of
only a couple of other elements in order to perform its functions. If any of
your elements need to use services from more than 50% of the other ele-
ments in the system, consider revising your functional structure.
C HECKLIST
Do you have fewer than 15 to 20 top-level elements?
Do all elements have a name, clear responsibilities, and clearly defined
interfaces?
Do all element interactions take place via well-defined interfaces and
connectors that link the interfaces?
Do your elements exhibit an appropriate level of cohesion?
Do your elements exhibit an appropriate level of coupling?
Have you identified the important usage scenarios and used these to
validate the system’s functional structure?
292
F URTHERP A R T III  A V I E W P O I N T C A T A L O G
Have you checked the functional coverage of your architecture to ensure
that it meets its functional requirements?
Have you defined and documented an appropriate set of architectural
design principles, and does your architecture comply with these principles?
Have you considered how the architecture is likely to cope with possible
change scenarios in the future?
Does the presentation of the view take into account the concerns and
capabilities of all interested stakeholder groups? Will the view act as an
effective communication vehicle for all of these groups?
R EADING
Many software architecture books focus on the functional aspects of architec-
ture, and the subject is (rightly) central to those that take a broader view. In
addition to the many books we mentioned in Parts I and II, the following are
relevant to the concepts we introduced in this chapter.
Clements et al. [CLEM03] is a detailed, thorough, and practical guide to
documenting various architectural styles. In the context of this chapter, the
discussions of overloading views and documenting the various styles of inter-
faces are particularly pertinent. Garland and Anthony [GARL03] describes
how to go about designing the software architecture for large-scale informa-
tion systems; the approach we suggest for modeling message-oriented ele-
ment interactions comes from this book. The techniques we outline for
element identification are based on the architectural “unit operations”
described in Bass et al. [BASS03], where they are described more fully.
Many good books explain UML in a tutorial style [FOWL03a, MILE06],
and there are a number that focus on how to use it to produce rigorous archi-
tectural descriptions [CHEE01, DSOU99]. Another timeless book that explains
how to produce rigorous models is [COOK94], now out of print but freely
available in PDF form (www.syntropy.co.uk/syntropy). Checkland [CHEC99]
presents an approach to understanding real user requirements, using an
informal diagrammatic approach called the “rich picture” (analogous to our
description of sketches) to help communicate with end users.
Meyer [MEYE00] is the definitive reference on Design by Contract (and
much more related to object orientation), and Mitchell and McKim [MITC02]
provides a nice, concise, practitioner-oriented introduction to the approach.
Wirfs-Brock et al. [WIRF90] is one of the original books on responsibility-
driven design, and a refinement to the approach by the same lead author can
be found in [WIRF02]. Finally, Shaw [SHAW94] is one of the first written
attempts to explain why connectors between elements are just as important to
models as the elements themselves.