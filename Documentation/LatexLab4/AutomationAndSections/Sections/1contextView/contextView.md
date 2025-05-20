16
T HEC ONTEXT V IEWPOINT
Definition
Concerns
Models
ProblemsPitfalls
and
Stakeholders
Applicability
Describes the relationships, dependencies, and interactions between the
system and its environment (the people, systems, and external entities
with which it interacts)
System scope and responsibilities, identity of external entities and services
and data used, nature and characteristics of external entities, identity and
responsibilities of external interfaces, nature and characteristics of external
interfaces, other external interdependencies, impact of the system on its
environment, and overall completeness, consistency, and coherence
Context model, interaction scenarios
Missing or incorrect external entities, missing implicit dependencies,
loose or inaccurate interface descriptions, inappropriate level of detail,
scope creep, implicit or assumed context or scope, overcomplicated
interactions, overuse of jargon
All stakeholders, but especially acquirers, users, and developers
All systems
Many architecture descriptions we’ve seen focus on views that model the sys-
tem’s internal structures, data elements, interactions, and operation. Archi-
tects tend to assume that the “outward-facing” information—the system’s
runtime context, its scope and requirements, and so forth—is clearly and un-
ambiguously defined elsewhere. In fact, in the first edition of this book, we
didn’t have a viewpoint for the system’s context for this very reason. How-
ever, we have decided that we were wrong! In practice, it often isn’t realistic
to delegate all of these concerns elsewhere, and you frequently need to in-
clude a definition of the system’s context as part of your architectural descrip-
tion. This can be for a number of reasons, including the following.
247
248
C ONCERNS
P A R T III  A V I E W P O I N T C A T A L O G
The system context is often implicit rather than being explicitly defined
as part of project initiation or requirements capture.
The system context may be loosely defined during requirements analysis,
but at a level of detail that means that you need to add signi ficantly to it.
You need to refer to elements of the system context elsewhere in your
architectural description, which makes it desirable for this information
to be part of the architectural description and so under your control.
In practice, most of the architectural descriptions we have created have
included a “context diagram,” which is essentially a view but without an associated
viewpoint definition to guide its structure and content. We therefore decided that
we should formalize the definition of context as we have for the other views.
The Context view of a system defines the relationships, dependencies,
and interactions between the system and its environment—the people, sys-
tems, and external entities with which it interacts. It defines what the system
does and does not do; where the boundaries are between it and the outside
world; and how the system interacts with other systems, organizations, and
people across these boundaries.
The Context view focuses on the outside world and usually represents the
system itself as a “black box,” hiding all details of its functional elements,
data, implementation, and so forth, since these are documented in one of the
other views.
System Scope and Responsibilities
This concern considers the main responsibilities of the system, that is,
what, in broad terms, it is required to do. For clarity, it may also identify
some specific exclusions, although by definition, anything not listed here
is excluded.
Note that this concern does not extend to a complete definition of the
system’s requirements, which is the responsibility of requirements analysis.
Scope definition should be brief, succinct, and easily understood by all stakehold-
ers without going into a lot of detail. It is usually defined in the form of a high-
level list of the system’s key capabilities or requirements, and it may also be use-
ful to highlight some functional exclusions explicitly for the avoidance of doubt.
Clear definition and agreement of scope are vital early milestones of any
system development project. Ideally the scope has already been defined for
you, in which case you may limit yourself to summarizing it in the Context
view and ratifying it with stakeholders as the AD develops. If the scope is not
defined, you may need to do this yourself, again based on input from your
stakeholders.
C H A P T E R 16  T H E C O N T E X T V I E W P O I N T
 249
EXAMPLE The scope definition for a simple online retailer might
include the following capabilities.
Present the retailer’s catalog to the user, including pictures and
product specifications
Provide a flexible search facility (search on product name, type,
keyword, size, and so on)
Accept orders for goods
Accept payment by credit card (with asynchronous approval and
notification to the customer)
Provide automated interfaces into back-end systems for
fulfillment
The exclusions for the first version of such a system might be:
The ability to amend or cancel orders (this will need to be done
over the telephone but is planned to be automated in a subsequent
release)
The ability to make payments by means other than credit card
Display of live stock levels and the ability to reserve out-of-stock
items
Identity of External Entities and Services and Data Used
An external entity is any system, organization, or person with which this
system interacts in some way, for example:
Another system that runs in the same organization as the system being
modeled (we refer to these as “internal systems”)
Another system that runs in another organization (we refer to these as
“external systems”)
A gateway or other implementation component that has the effect of
hiding other systems (which may themselves be internal or external)
A data store that is external to the system (for example, a shared database
or data warehouse)
A peripheral or other physical device that is external to the system (such
as a shared messaging appliance or enterprise search engine)
A user, a class of user, or some other person or role, such as operational
or support staff
250
 P A R T III  A V I E W P O I N T C A T A L O G
Each external entity will implement and offer some services, and manage
and provide some data, that are used by this system. Similarly, each external
entity will use some services and/or data offered by this one. External entities
that do none of these things are not normally of interest.
Note that in this chapter we use the term services to refer to functionality
that systems provide for each other. This important concept is relevant
whether it is implemented by a formal service-oriented architecture (SOA), or
some other, more traditional means such as messaging or file transfer.
Nature and Characteristics of External Entities
The quality properties of external entities, such as system stability and availability,
performance and throughput capabilities, physical location, or data quality, may
significantly affect the architecture of the system.
EXAMPLE A travel booking system exchanges information with many
other systems located around the world. Some of these systems in more
exotic locations may be only intermittently available, because of time
zone differences or because they are more liable to failure. However, a
failed communication with such a system might result in a customer’s
booking being lost, which is highly undesirable.
The travel system’s interfaces with external systems will therefore need
to be carefully designed. All failed interactions should be automatically re-
tried a configurable number of times, and these retry attempts should be
logged to a database so that operational staff can monitor trends. Interac-
tions will need to be designed so that they can potentially be submitted
multiple times without error (this is known as “idempotence”). It should
be possible to restart very large transfers that fail partway through from
the point of failure rather than having to retransmit the whole file.
The quality properties to be considered are exactly those properties that
are defined in Part IV of the book, The Perspective Catalog.
However, it is only the “externally visible” properties that need to be
considered—it is not normally necessary to consider the internal properties of
external systems. For example, an external system may have some unreliable
internal components but mask this to the outside world using load distribution
techniques to give a high level of external availability. Similarly, you need only
consider those interfaces that you will need to use—it is not necessary to
understand or document every interface of each of your external entities.
It may be necessary to consider the “nature” of external entities that are
not systems. For example, a user may not speak the primary language of the
system, or a peripheral such as a shared fax gateway may have performance
characteristics that need to be taken into consideration.
C H A P T E R 16  T H E C O N T E X T V I E W P O I N T
 251
Identity and Responsibilities of External Interfaces
For each external entity, the nature of all interfaces between it and this sys-
tem should be identified. Such an interface may serve one of the following
purposes.
Data provider or consumer: The external system supplies data directly to
this system or receives data directly from it.
Service provider or consumer: The external system is requested to per-
form some action by this system or requests some action of this system
(e.g., a service call), and the service may return data and/or status infor-
mation in response to the request.
Event provider or consumer: The external system publishes events that
this system wishes to be notified of, or this system publishes events that
the external system wishes to be notified of.
For data provider and consumer interfaces, the concern identi fies the content,
scope, and meaning of the data to be transferred.
For service interactions, the concern identifies the semantics of the request
(the nature of what is being requested and any parameters); the actions to be
taken by the system fulfilling the request; any data to be returned; any acknowl-
edgment, status, or error information that may be returned; and any exception
actions to be taken by either side.
For event provider and consumer interfaces, the concern identi fies the
events of interest, their meaning and content, and the volume and likely timing
of their occurrence.
It may be appropriate to go into more detail for more complex interactions
between this system and external entities, such as a payment authorization
which must be followed by a payment request.
Nature and Characteristics of External Interfaces
The quality properties of external interfaces may differ significantly from the
quality properties of the systems at the other end. For example, there may be
a low-bandwidth, relatively unreliable data link to a highly resilient system in
another country. The interface is the constraining factor in this case and
again will have a significant effect on the architecture of the system.
System characteristics include the following:
The expected volumes—number of requests or transfers, size of data,
seasonal fluctuations, and expected growth over time
Whether interactions are scheduled (occurring at predefined times), oc-
cur in response to events, or are ad hoc
252
 P A R T III  A V I E W P O I N T C A T A L O G
Whether interactions are completely automated, completely manual (e.g.,
a user saves a file or sends an e-mail), or somewhere in between
Whether interactions are transactional—that is, they are required to com-
plete fully or not at all
Criticality and timeliness—for example, a particular interaction that may
be required to complete before the end of the business day in order to be
captured by an auditing or accounting system
Whether interactions are batch (large data sets transferred as a “unit”),
message-based, or streaming in nature
What level of security is required (authentication, authorization,
confidentiality, and so on)
The service level that can be expected of the interface (in terms of
response time, latency, scalability, availability, and so on)
The technical nature of the interface and what protocols are used (open
standards or proprietary)
Data and file formats
Again, you can use the material in Part IV (The Perspective Catalog) to
frame your analysis.
Other External Interdependencies
There may be interdependencies between this system and external entities
other than data flows or function invocations. These interdependencies may
act in either direction—the system may be dependent on an external entity or
vice versa. Such dependencies can be subtle and are sometimes hard to find.
This concern identifies the nature of the dependency and may also articulate
its architectural impact—that is, what capabilities or features need to be built into
the architecture in order for the dependency to be observed.
EXAMPLE An online retailer accepts orders for goods over the Internet
through its main e-commerce system. However, to ful fill an order, this
system has to interact with a separate payment system to collect
payments, a customer account details system to make any updates to
the customer’s account (such as shipping addresses), and a ful fillment
system that dispatches the goods.
From the perspective of the e-Commerce System, it is dealing with three
separate independent systems and can treat them as such. However, as can
be seen in Figure 16–1 there is a data dependency between two of them that
in certain situations must be taken into account. The Fulfillment system
in this organization contains its own list of verified dispatch addresses for
each customer, and it will reject orders that are not being sent to these
addresses. However, this list is maintained by data replication from the
Customer Accounts System. When the workflow for a customer order
involves updating dispatch addresses, the e-Commerce System must take
this dependency, and the latency of the replication, into account. Otherwise,
orders may be rejected by the Fulfillment System because their dispatch
addresses are not listed in its database.
The architectural impact in the case of this system might be to allow
for resubmission to the Fulfillment System after a delay if a fulfillment
request is rejected or to delay orders that have associated address up-
dates to allow data replication to occur. (Interestingly, having talked
about the need to understand the details of external interfaces, this is an
example that bears this out: The tactic of resubmitting failed orders can
be made much more efficient if the interface to the Fulfillment System
allows the reason for failure to be reliably discerned from the dispatch
status returned by that system.)
Impact of the System on Its Environment
This concern addresses the impact of the system’s deployment on its environ-
ment, both within the organization in which it is deployed and externally.
This includes the following:
254
 P A R T III  A V I E W P O I N T C A T A L O G
Any systems that are dependencies and so may require functional
changes, interface changes, or performance or security improvements
Any systems that will be decommissioned (switched off) as a result of
this system’s deployment
Any data that will be migrated into this system
Although these changes may be someone else’s responsibility, they
should still be itemized to ensure that they are being addressed by someone
and their progress tracked. (We return to this issue in our discussion of func-
tional migration and data migration in Chapter 21.)
Overall Completeness, Consistency, and Coherence
In most cases this system will be part of something much larger: the overall
“application landscape.” This may even extend to systems distributed across
multiple organizations and linked together over private or public networks. Such
application landscapes can be very complex and are often poorly understood.
A key concern of your stakeholders (particular users) is that the overall end-
to-end solution provides them with the functionality that they need in a sensible
way, irrespective of which system provides a specific piece of functionality or
manages a specific piece of data.
EXAMPLE In the early days of Internet shopping, retailers worked hard to
get their catalogs onto the Internet in a pleasing and visually compelling
way. The overriding concern was to get shoppers to visit their site rather
than a competitor’s. However, many of these retailers did not put nearly as
much effort into the behind-the-scenes processes for accepting payment,
fulfilling orders, or dealing with exceptions. As a result, they lost customer
goodwill and gained a reputation for poor customer service, and in the
most extreme cases they went out of business.
TABLE 16–1 S TAKEHOLDER C ONCERNS FOR THE C ONTEXT V IEWPOINT
Stakeholder Class
 Concerns
Acquirers
 System scope and responsibilities, identity of external entities and ser-
vices and data used, impact of the system on its environment
Assessors
 All concerns
Communicators
 System scope and responsibilities, identity and responsibilities of
external entities, identity and responsibilities of external interfaces
C H A P T E R 16  T H E C O N T E X T V I E W P O I N T
 255
TABLE 16–1 S TAKEHOLDER CONCERNS FOR THE CONTEXT V IEWPOINT (C ONTINUED)
Stakeholder Class
 Concerns
Developers
 All concerns
Production engineers
 Nature and characteristics of external interfaces, impact of the system
on its environment
System administrators
 All concerns
Testers
 All concerns
Users
 System scope and responsibilities; identity of external entities and ser-
vices and data used; overall completeness, consistency, and coherence
While this concern is more the responsibility of the enterprise architect than
of the application architect (see Chapter 5), giving it some consideration will
improve your likelihood of success, possibly significantly. An overall solution that
hangs together in a consistent and coherent way is much more likely to delight
your users than one that is fragmented and misaligned.
At a minimum you should ensure that the main business processes appear to
have adequate coverage, with either systems or defined manual processes. Simi-
larly, all of the data required for these processes should be stored somewhere (in
this system or externally) and be accessible by those systems that need it.
M ODELS
Stakeholder Concerns
Typical stakeholder concerns for the Context viewpoint include those listed
in Table 16–1.
Context Model
The context model is the main architectural model within the Context view and
often the only one produced. It places the system clearly in its environment and
relates it to the external entities with which it interacts, via explicit relation-
ships that represent the interfaces to and from it.
The purpose of the context model is to explain what the system does and
does not do, to present an overall picture of the system’s interactions with the
outside world, and to summarize the roles and responsibilities of the partici-
pants in these interactions. This understanding is essential in order to make
sure that all who are involved in the development of the system (and in mak-
ing any necessary changes outside of it) know what they are responsible for
and exactly where the boundaries are. This avoids potential duplication of de-
velopment effort or, even worse, gaps or inconsistencies in the solution.
256
 P A R T III  A V I E W P O I N T C A T A L O G
The context model has a wide audience, being of significant interest to all
of the system’s stakeholders. For this reason it should use simple, familiar
terms, avoid business or technology jargon, and aim for simplicity without
abstracting away so much information as to be worthless. It often uses busi-
ness language to name and describe the elements within it and typically
focuses on overall functionality and information flow, rather than the tech-
nologies used to implement them.
The context model is usually fairly high-level and abstract, answering the
important “why” and “what” questions about the architecture. It does not specify
in any detail how the system or its interfaces will be built; these questions are
answered in the other architectural views.
The context model presents an overall picture of the system in its environment
and typically includes the following types of elements:
The system itself, represented as a black box, with its internal structure hid-
den, since the Context view is not concerned with how the system is built.
The external entities, represented as black boxes for the same reason.
(Indeed, it is likely that the internal details of external entities are not
visible or known.) For each external entity, it is important to capture
some key information, namely, the name of the entity, the nature of the
entity (e.g., system, data store, person, group), the owner of the entity,
and the responsibilities of the entity from the perspective of this system
(the services, functions, and data upon which this system relies).
The interfaces between the system and the external entities, presented at a
summary level, highlighting the key data items or function invocations
across the interface. Often all of the individual interfaces between the
system and each external entity are “rolled up” into a single interface, to
make the diagram easier to follow. For each external interface it is impor-
tant to capture an overview of the interactions expected over the interface,
the semantics of the interface (i.e., the data exchanged and its meaning),
the exception processing approach that will be used when unexpected
things happen, and the key quality properties of the interface upon which
this system is relying. In many cases, you will just capture a short sum-
mary of this information in the context model and reference external
sources of information for fuller descriptions.
The context model is a vital communication tool with a wide range of
stakeholders from business and technology. It is often used to summarize
“what the project is about,” identify who the external partners are, and ex-
plain the interactions with them. Since it has a wide audience of varying de-
grees of business and technical expertise, it should be kept relatively simple,
and the context diagram should fit on a single page if possible.
N OTATION The two notations that we commonly see used for context models
are UML and “boxes–and–lines.”
Unfortunately, the UML standard doesn’t define a context diagram. The
assumption seems to be that the context of the system will be captured using
a “use case” diagram, with the boundary of the system being represented by a
classifier (class, component, or package) that contains the use cases, or sim-
ply by a diagrammatic annotation such as a rectangle drawn around the use
cases. However, there are a number of practical difficulties with this ap-
proach, including the complexity of the resulting diagram, the fact that the
use case list may not be available when the context diagram is created, and
the convention that the external interfaces are made to specific use cases. In
the context diagram, we really want to abstract this detail away and treat the
system as a black box.
The solution to these difficulties is to create a UML diagram of the form
shown in Figure 16–2.
This sort of UML diagram can be created using the “use case” or “class
diagram” diagram editors of many mainstream UML modeling tools, although
in fact it doesn’t share a lot of similarity with either standard diagram. The
key points about it are as follows:
258
 P A R T III  A V I E W P O I N T C A T A L O G
The system is represented as a UML component, stereotyped as a subsystem,
a stereotype found in the UML standard profile, or with a more specific
stereotype that you create yourself.
External entities that cause human interactions with the system are
represented as UML actors.
External entities that are systems are represented as either further subsystem
components or actors, possibly with their icons changed via stereotyping to
be more representative of the entities that they represent (as suggested by the
UML standard).
Interfaces between the external entities and the system being designed can
be represented as UML information flows, UML dependencies, or UML as-
sociations, optionally augmented with UML “conveyed information” icons
that define the information flowing over the interface (which we don’t
show in the example but would be represented as small black arrowheads
on the associations).1
While UML can be used to create a context diagram, it would be fair to say
that the language does not provide particularly strong support for this type of
model. For this reason, we often use informal boxes–and–lines notation
instead, drawing something more akin to a “rich picture” of the system’s
context using a simple, ad hoc notation (and it’s obviously important to de fine
the notation clearly). Figure 16–3 shows the same system represented in
boxes-and-lines notation.
The advantage of this style of diagram is that it can be much more expressive
than plain UML, and it’s probably easier for most people to create and understand
than one created in strict accordance with UML. One of the major disadvantages,
apart from your having to design and explain the notation, is that this model (or
picture) is separate from the rest of your architectural models, assuming they’re in
UML. However, a number of UML modeling tools can now draw this sort of infor-
mal picture, which largely addresses this concern.
ACTIVITIES Definition of context takes place very early in the project
lifecycle and is often rather ad hoc and unstructured as a result. It is also
rarely under the control of the architect—you will be a participant and will
provide input and feedback, but key decisions will probably be made by the
senior stakeholders (typically the acquirer and some senior users).
It is possible to put some level of formality in place, however. At a mini-
mum, a single document should be maintained and lodged in a place where
everyone who needs access has it. It may be necessary to restrict access to the
1. Information flows and conveyed information annotations were introduced as part of UML
2. They are supported at differing levels of fidelity by different modeling tools, but they are
a valid part of the metamodel, defined in the “Superstructure” specification [OMG10b].
document to key personnel if the project is sensitive (for example, if it will
lead to the retirement of existing systems or has contractual implications with
suppliers). If possible, historical versions of the document should be retained
along with a log of who changed what.
You will typically go through the following steps when preparing a
context model.
Review the goals of the system: Briefly review and capture the business
and technology goals of the system—for example, “Reduce cost per
transaction by 15%,” “Streamline the ordering and fulfillment process,
enabling better customer service,” “Replace the current architecture with
one that is more performant, resilient, and amenable to change,” and so
260
 P A R T III  A V I E W P O I N T C A T A L O G
on. The goals should make the motivation for the project clear, illustrat-
ing how its implementation will improve the current situation, in terms
that the acquirer and other key stakeholders can understand.
Review the key functional requirements: Briefly review and summarize
the key requirements that characterize what the system must do,
grouped by subject area. Use the scope definition for this.
Identify the external entities: Itemize all internal and external systems,
gateways, services, external data stores, devices, appliances, and users
and roles that may interact with the system. You will need to use your
own and others’ knowledge of the business area, and any existing docu-
mentation such as system diagrams or organizational charts. At this
stage, if there is any doubt as to whether an entity should be included,
include it—you can always take it out later.
Define responsibilities of external entities: Use your and your stakehold-
ers’ knowledge of the entities to map out their expected responsibilities. If
there are any responsibilities that you find you can’t assign to the system
or an external entity, you have missed something in the system’s context.
Identify the interfaces between this system and each external entity : Use
your and your stakeholders’ knowledge of the processes the system will
implement to identify the data flows and service invocations (in either
direction) that these will require. Again, the scope definition will help
make sure you don’t miss anything.
Identify and validate the interface definitions: Make sure that each inter-
face is defined (perhaps in the AD but probably elsewhere) and that it is
compatible with the use to which it will be put. If the interface is docu-
mented elsewhere, make sure you reference it in the AD.
Walk through key requirements: Follow the flow of control and flow of
information between the system and the external entities. As you do this,
add all the external interfaces that are needed to implement these flows.
Walk through scenarios or use cases: If you have more detailed scenario
definitions or use cases, walk through these to validate the model. Add
or update any external entities or interfaces required.
Interaction Scenarios
It is often useful to model some of the expected interactions between your system
and the external entities in more detail than is provided in a context diagram.
This sort of model helps to uncover implicit requirements and constraints (such
as ordering, volume, or timing constraints) and helps to provide a further, more
detailed level of validation. While you are unlikely to have time to model all the
scenarios in which your system will participate, it can be useful to model some of
the more complicated, contentious, or less well-understood ones, especially when
system usage is unclear or there is disagreement among your stakeholders.
C H A P T E R 16  T H E C O N T E X T V I E W P O I N T
 261
An interaction scenario represents two or more participants (usually the
system and one or more external entities), and a sequence of interactions
between them, where an interaction is a flow of information and/or a request
to perform an action. The interactions should collectively serve a specific pur-
pose or implement a specific function. Refer to Chapter 10 (Identifying and
Using Scenarios) for more detail.
N OTATION
 Interaction scenarios are usually captured using simple tex-
tual interaction lists (rather like those used for use case defi nitions) or UML
sequence diagrams that illustrate the interactions via a graphical notation.
More detail is given in Chapter 10.
ACTIVITIES Refer to the discussion of scenarios in Chapter 10.
P ROBLEMSAND P ITFALLS
Missing or Incorrect External Entities
Most systems development projects tend to be relatively chaotic in their early
stages (their teams are in their “forming” or “storming” stages in Tuckman’s
model of group development). Roles, even senior roles, may not be formally
defined, and as a result context is often unclear and subject to frequent
change. It is therefore easier than you might think to accidentally leave some-
thing out of the context model, include something that is not needed, or put
the system boundaries in the wrong place.
Getting the context wrong can have a huge impact later on: Either the
project will have to undergo significant change at a late point in the lifecycle,
which adds considerably to its cost, duration, and complexity; or the delivered
system will be incomplete or provide unnecessary functionality.
RISK REDUCTION
Work with a wide range of stakeholders to ensure that their concerns are
adequately reflected in the context model and interaction scenarios. For
example, you should ensure that any functionality they require either is
part of the system scope, is provided by an external entity, or is excluded
entirely with the agreement of the people who need it.
Involve a domain expert in this analysis as early as you can, and make
sure that person is involved in review and sign-off of this part of the AD.
Ensure that once the context model has stabilized, it is change-managed
and subsequent changes to it are reviewed and agreed upon.
262
 P A R T III  A V I E W P O I N T C A T A L O G
Missing Implicit Dependencies
It is easy to miss the subtle dependencies between external entities. For exam-
ple, you may assume that a particular business entity or data item is instanta-
neously available in two external systems, when there is actually a signi ficant
latency due to the mechanics of data transfer. Or you could assume that the
availability of an external system will affect only one part of your system,
whereas in fact other systems you rely on are also dependent on it, making its
nonavailability much more important to you. Such implicit dependencies can be
hard to understand yet may have significant implications for the architecture.
They should therefore be captured early and documented clearly.
RISK REDUCTION
 Assume nothing, work with your stakeholders to uncover and under-
stand implicit dependencies, and ensure that they are documented in the
Context view.
Loose or Inaccurate Interface Descriptions
It’s tempting to get the basic idea of an external interface and leave it at that,
hoping that the design process will elicit the details. In fact, you always have to
do this to some extent as you can’t understand every detail of every inter face.
However, it is important that you capture enough detail so that the architec-
tural implications can be understood.
RISK REDUCTION
 Ensure that you understand your external interfaces in suf ficient
detail to use them confidently, and capture enough information about
them in the Context view to characterize the effect they have on your
architecture.
Avoid the temptation to gloss over things that are complex in the expectation
that problems will be resolved later.
Inappropriate Level of Detail
Getting the level of detail right is a challenge everywhere in the AD but is
especially important in the Context view. If you provide too much detail,
stakeholders, especially senior stakeholders like the acquirer, may become
overwhelmed and fail to understand the big picture. Conversely, if you gloss
over some aspects of the context or scope, expecting them to be fleshed out
later, you may miss something important, mislead your stakeholders, or allow
incorrect assumptions to be made.
C H A P T E R 16  T H E C O N T E X T V I E W P O I N T
 263
RISKREDUCTION
Look out for scope or requirements that appear vague because
nobody understands what they mean (or people assume different
meanings) and explore them further in order to ensure that they are
understood.
If the Context view becomes too detailed, move some of the information
either into appendices in the document or into another view in the AD
(typically the Functional or Information view).
Consider applying some rules of thumb to determine whether your Context
view is becoming too detailed. Although every situation varies, we have
found the following rules to be useful in practice:
• A context diagram should usually fit on a single sheet of paper.
• A scope definition should not usually be more than 2 to 3 pages.
• If there are a lot of requirements, they should be grouped by functional
area, organizational responsibility, or some other logical category.
• If there are more than, say, 10 to 20 external entities, consider
whether they can be grouped by type (for example, a large number of
suppliers of the same type of goods), or whether you really have a
single system at all, rather than a collection of systems.
Scope Creep
Scope creep is the phenomenon of uncontrolled changes to system scope, which
often occur gradually without being particularly visible to stakeholders. These
changes usually have the effect of increasing what the system is expected to do,
often without due consideration of whether this is sensible or achievable. For ex-
ample, when interviewing users about required functionality, it is easy for each
user to add a few more requirements to the mix that really are “nice-to-haves”
rather than truly essential. By the time this process is completed, the system is
significantly larger and more ambitious, possibly fatally so.
Scope creep can also occur once the scope has stabilized if it is not subject
to well-managed change control.
RISK REDUCTION
 Challenge additions or changes to scope to confirm that they really are
necessary and make sure their implications are understood.
 Work to help stakeholders understand the consequences of adding
requirements, such as increased time to market, development and
operational cost, or system complexity and stability.
 Ensure that scope changes are change-managed once the scope has
stabilized.
264
 P A R T III  A V I E W P O I N T C A T A L O G
Implicit or Assumed Context or Scope
More than in any other part of the AD, the scope definition is where you
should state the obvious when there is any chance of misunderstanding.
Don’t be tempted to leave things out because “everybody knows that”—the
odds are that there are some stakeholders who don’t, or that some of these
nuggets of information will get lost along the way.
RISK REDUCTION
 Don’t be afraid to state the obvious in the Context view. You will be glad
you did later on!
Overcomplicated Interactions
Interactions with some external entities (particularly older systems) can be a lot
more complicated than expected, so it’s easy to end up with unexpected prob-
lems when you come to build the interfaces. For example, some of the problems
we have encountered when dealing with interfaces to long-established systems
have included the need for unusual data encodings, poorly understood (yet
complicated) conversational protocols, and complex and proprietary interface
technologies that can cause difficulties for development, testing, and opera-
tional activities.
RISK REDUCTION
 Take the time to understand interfaces to external systems early in the
architectural design process, and don’t assume that they’re necessarily
the same as the interfaces you’ve met before.
 Find expertise in the interfaces that you need to use, prototype interac-
tions with them, and test them thoroughly in order to understand how
they behave in different situations.
Overuse of Jargon
Inputs to the Context view come from a wide variety of sources. It is easy, there-
fore, to make careless use of business and technology terminology that may not
be well understood by the majority of your stakeholders. Since people are often
reluctant to question things they do not understand, you risk confusion and
misunderstanding.
RISK REDUCTION
 Try to avoid any terminology that is not widely understood. If you need
to use jargon and there is any risk of confusion, provide a glossary.
C HECKLIST
C H A P T E R 16  T H E C O N T E X T V I E W P O I N T
 265
Have you consulted with all of the stakeholders who are interested in the
Context view (which is probably all of them)?
Have you identified all of the external entities with which the system
needs to interact and their relevant responsibilities?
Do you have a good understanding of the nature of every interface with
each external entity, and is this documented to an appropriate level of
detail?
Have you considered possible dependencies between the external entities
with which you have to interact? Are these implicit dependencies documented
in the AD?
Does the context diagram adequately illustrate all of the interfaces from
the system to its environment, with sufficient definition underpinning
the diagram?
Have all key stakeholders formally agreed to the content of the context
model? Is this documented somewhere?
Has the context model been placed under formal change control?
Is the change control process being followed? Are stakeholders being
consulted on changes and their consent obtained?
Is the context model placed somewhere where everyone can easily find it,
such as a public shared folder or wiki page?
Have you identified all of the key capabilities or requirements of the
system, and are they documented to an appropriate level of detail?
Is the scope definition internally consistent?
Does the scope identify any important technology constraints, such as
mandated platforms?
Is the scope specified at an appropriate level of detail, balancing brevity
with clarity and completeness?
Have you explored a set of realistic scenarios for external interactions
between your system and external actors?
Are other teams with which you interact clear on the context and scope
and any implications for them?
Have you checked the context model to see if there are any “obvious”
statements that should be explicitly stated but have been omitted?
Do the main business processes appear to have adequate coverage, by
either systems or defined manual processes?
Does all the data needed to support the main business processes appear
to be stored somewhere, on-site or externally?
Does the overall solution hang together in a coherent way?
266
 P A R T III  A V I E W P O I N T C A T A L O G
F URTHER R EADING
Many software architecture books discuss the process of setting the context of
the system; examples include Garland and Anthony [GARL03], which describes
a Context viewpoint, and Bosch [BOSC00], which describes how to define the
system context at the start of the architectural design process.
A number of requirements engineering books also discuss scoping systems.
A particularly good example is Sommerville and Sawyer [SOMM97], which
presents a clear set of guidelines around requirements capture, presentation,
and ratification. Each guideline is accompanied by a cost/benefit analysis and
practical suggestions for how it can be implemented.
Information on Tuckman’s model of group development can be found in
[TUCK65] and elsewhere.