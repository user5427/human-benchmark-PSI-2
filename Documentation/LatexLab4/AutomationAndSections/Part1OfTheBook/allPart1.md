2
S OFTWAREC ONCEPTS
A RCHITECTURE
O
ne is that of the the problems terminology when has we been talk loosely about architecture borrowed from for software other disciplines
 systems
(such as building architecture or naval architecture) and is widely used, in-
consistently, in a variety of situations. For example, the term architecture is
used to refer to the internal structure of microprocessors, the internal struc-
ture of machines, the organization of networks, the structure of software pro-
grams, and many other things.
This chapter defines and reviews some of the core concepts that underpin
the discussion in the remainder of the book: software architecture, architec-
tural elements, stakeholders, and architectural descriptions.
SOFTWARE A RCHITECTURE
Computers can be found everywhere in modern society—not just in data centers
or on desks but also in cars, washing machines, cell phones, and credit cards.
Whether they are big or small, simple or complex, all computer systems are
made up of the same three fundamental parts: software (e.g., programs or
libraries); data, which may be either transient (in memory) or persistent (on
disk or ROM); and hardware (e.g., processors, memory, disks, network cards).
D EFINITION When we refer to a computer system, we mean the software el-
ements that you need to specify and/or design in order to meet a particular set
of requirements and the hardware that you need to run those software
elements on.
11
12
 P ART I  A RCHITECTURE F UNDAMENTALS
When you try to understand a system, you are interested in what its individual
parts actually do, how they work together, and how they interact with the world
around them—in other words, its architecture. A widely accepted definition of soft-
ware architecture can be found in the recent international standard ISO/IEC 42010,
“Systems and Software Engineering—Architecture Description” [ISO11].
DEFINITION The architecture of a system is the set of fundamental concepts
or properties of the system in its environment, embodied in its elements, rela-
tionships, and the principles of its design and evolution.
Let’s look at three key parts of this definition in a bit more detail, namely,
a system’s elements and relationships, its fundamental properties, and the
principles of its design and evolution.
System Elements and Relationships
Any system is composed of a number of pieces, which may be called things
like module, component, partition, or subsystem. We deliberately avoid using
any of these terms because they all have connotations suggesting certain
types of implementation or deployment technology. We prefer to follow the
lead of the ISO standard and a number of others and use the less familiar but
semantically neutral term elements to refer to the pieces that constitute a sys-
tem. We’ll define the term architectural element more formally later in this
chapter, but at this stage let’s just agree that elements are the architecturally
significant pieces of a system.
The elements that constitute a system and the relationships between them de-
fine the structure of the system that contains them. There are two types of struc-
tures that are of interest to the software architect: static structure (organization of
design-time elements) and dynamic structure (organization of runtime elements).
1. The static structures of a system tell you what the design-time form of
a system is—that is, what its elements are and how they combine to
provide the features required of the system.
DEFINITION The static structures of a system define its internal design-
time elements and their arrangement.
Internal design-time software elements might be programs, object-
oriented classes or packages, database stored procedures, services, or
any other self-contained code unit. Internal data elements include
classes, relational database entities/tables, and data files. Internal
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 13
hardware elements include computers or their constituent parts such as
disk or CPU and networking elements such as cables, routers, or hubs.
The static arrangement of these elements defines—depending on the
context—the associations, relationships, or connectivity between these
elements. For software modules, for example, there may be static rela-
tionships such as a hierarchy of elements (module A is built from mod-
ules B and C) or dependencies between elements (module A relies on the
services of module B). For classes, relational entities, or other data ele-
ments, relationships define how one data item is linked to another one.
For hardware, the relationships define the required physical interconnec-
tions between the various hardware elements of the system.
2. The system’s dynamic structures show how the system actually works—
that is, what happens at runtime and what the system does in response
to external (or internal) stimulus.
D EFINITION The dynamic structures of a system define its runtime ele-
ments and their interactions.
These internal interactions may be flows of information between ele-
ments (element A sends messages to element B) or the parallel or se-
quential execution of internal tasks (element X invokes a routine on
element Y), or they may be expressed in terms of the effect they have on
data (data item D is created, updated many times, and finally destroyed).
Of course, a system’s static and dynamic structures are closely related to one
another. For example, without static structure elements such as programs or data-
bases, there would not be any dynamic structure elements for information to flow
between. However, the two types of structures are not the same. Consider a simple
client/server system with one client-facing element that handles all interactions
with users. This would appear once as a static structure element but would appear
many times (once per active user) in a dynamic structure model. The dynamic
structure model would also have to explain what caused the instances of the client
element to become active or inactive (e.g., a user logging in and logging off again).
Fundamental System Properties
The fundamental properties of a system manifest themselves in two different
ways: externally visible behavior (what the system does) and quality proper-
ties (how the system does it).
1. Externally visible behavior tells you what a system does from the stand-
point of an external observer.
14
 P ART I  A RCHITECTURE F UNDAMENTALS
DEFINITION The externally visible behavior of a system defines the func-
tional interactions between the system and its environment.
These external interactions form a set similar to the ones we consid-
ered for dynamic structure. This includes flows of information in and out
of the system, the way that the system responds to external stimuli, and
the published “contract” or API that the architecture has with the outside
world.
External behavior may be modeled by treating the system as a
black box so that you don’t know anything about its internals (if you
make request P to a system built in compliance with the architecture,
you are returned response Q). Alternatively, it may consider changes to
internal system state in response to external stimuli (submitting a
request R causes the creation of an internal data item D).
2. Quality properties tell you how a system behaves from the viewpoint of an
external observer (often referred to as its nonfunctional characteristics).
DEFINITION A quality property is an externally visible, nonfunctional prop-
erty of a system such as performance, security, or scalability.
There is a whole range of quality properties that may be of interest:
How does the system perform under load? What is the peak throughput
given certain hardware? How is the information in the system protected
from malicious use? How often is it likely to break? How easy is it to man-
age, maintain, and enhance? How easily can it be used by people who are
disabled? Which of these characteristics are relevant depends on your cir-
cumstances and on the concerns and priorities of your stakeholders.
Principles of Design and Evolution
One of the things that is immediately obvious about a well-structured and
maintainable system is that its implementation is consistent and respects a
system-wide set of structuring conventions. This allows the system to be
more easily understood and encourages extensions to the system to be made
in a consistent and logical way, fitting into the overall form of the system
without introducing unnecessary complexity.
One of the things that is necessary in order to achieve this internal imple-
mentation consistency is a clear set of principles to guide the system’s design
and evolution.
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 15
According to the Oxford English Dictionary, the general definition of a prin-
ciple is a fundamental truth or proposition serving as the foundation for belief
or action. In the context of architectural design, we extend this definition
slightly and define an architectural principle to be a fundamental statement of
belief, approach, or intent that guides the definition of your architecture .
Defining and following architectural principles is a powerful way of estab-
lishing a decision-making framework for a consistent, well-structured archi-
tecture. Principles expose underlying assumptions and bring them out into
the cold light of day—in other words, they make the implicit explicit. They are
a great way to kick off an architecture project, especially when motivation or
scope is unclear. They are also useful if you suspect that there are significant
but unrecognized conflicts or contradictions in the requirements of a proposed
architecture. We’ll have quite a lot more to say about design principles in
Chapter 8.
System Properties and Internal Organization
Let’s explore the idea of system properties and how they are related to the
internal organization of a system by means of a simple example.
EXAMPLE An airline reservation system supports a number of different
transactions to book airline seats, update or cancel them, transfer them,
upgrade them, and so forth. Figure 2–1 shows the context for this sys-
tem. (We have used a simplified use case notation here: The rectangle
represents the system, the “stick man” represents customers who inter-
act with the system, and the notation boxes provide additional support-
ing information.)
The externally visible behavior of the system (what it does) is its re-
sponse to the transactions that can be submitted by customers, such as
booking a seat, updating a reservation, or canceling a booking. The
quality properties of the system (how it does it) include the average re-
sponse time for a transaction under a specified load; the maximum
throughput the system can support; system availability; and the time,
skills, and cost required to repair defects.
Faced with these requirements, there are a number of ways that an archi-
tect could design a system for it. Over the next few pages we outline two pos-
sible architectural approaches for this system.
16
P ART IA RCHITECTURE F UNDAMENTALS
Users of
the system
Customers
«system»
Airline Reservation System
System under
consideration
FIGURE 2–1 CONTEXT D IAGRAM FOR AN A IRLINE B OOKING SYSTEM
The architect could design a solution for the airline reservation sys-
tem based around a two-tier client/server approach. (In fact, this is an
example of the use of an architectural style, as we will see in Part II.) In
this approach, shown in Figure 2–2, a number of clients (which present
information to customers and accept their input) communicate with a
central server (which stores the data in a relational database) via a
wide-area network (WAN). An established architectural style like two-
tier client/server has widely known benefits and pitfalls, so starting like
this with a well-understood approach helps to avoid introducing unnec-
essary risk to the design.
As the diagram illustrates, the static structure (design-time organiza-
tion) for this client/server architecture consists of the client programs
(which in this example are further broken down into presentation, busi-
ness logic, database, and network layers), the server, and the connec-
tions between them. A related architectural diagram would show that
the dynamic structure (runtime organization) is based on a request/re-
sponse model: Requests are submitted by a client to the server over the
WAN, and responses are returned by the server to the client. The static
elements of the architecture provide the mechanisms whereby the dy-
namic interactions can occur (for example, the client programs submit
requests on behalf of the users and receive and display the results).
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 17
Clients
n
o s
 e
i k
t s s ra e c
 i a otn n i g b we s o a ts u L t a ee r B D NPn
o s
 e
i k
t s s ra e c
 a oit n n i g b we s o a t ts u L a ee r B D NPn
o s
 e
i k
t s s ra e c
 a oit n n i g b we s o a t ts u L a ee r B D NPWAN
Database
Server
FIGURE 2–2 TWO -TIER C LIENT/S ERVER A RCHITECTURE FOR A N AIRLINE B OOKING SYSTEM
Alternatively, the architect could take a three-tier client/server ap-
proach, where only the presentation processing is performed on the cli-
ents, with the business logic and database access performed in an
application server, as shown in Figure 2–3.
The static structure for this architecture consists of the client pro-
grams (which in this example are further broken down into presentation
and network layers), the application server (here, business logic, data-
base, and network layers), the database server, and the connections
between them. The dynamic structure is based on a three-tier request/
response model: Requests are submitted by a client to the application
server over the WAN, the application server submits requests to the
database server if necessary, and responses are returned by the applica-
tion server to the client.
The architect might identify the two-tier approach as appropriate for
the architecture because of its relative operational simplicity, because it
can be developed quickly by the organization’s software developers,
because it can be delivered at lower cost than other options, or for a
range of other reasons.
18
 P ART I  A RCHITECTURE F UNDAMENTALS
Clients
n
oitatneserPk
rowteNn
oitatneserPk
rowteNn
oitatneserPk
rowteNWAN
Application
Server
Business Logic
Database
Network
Database
Server
Database
Storage
FIGURE 2–3 THREE -T IER C LIENT /SERVER A RCHITECTURE FOR AN A IRLINE BOOKING SYSTEM
Alternatively, the architect may consider the three-tier approach to be
right for the architecture because it provides better options for scalability
as workload increases, because less powerful client hardware is needed,
because it may offer better security, or for other reasons.
Whichever approach the architect considers to be more appropriate,
she chooses it because it provides the best match between the system
properties promised by the approach and the requirements of the
system.
In this example, there are two possible solutions to the problem, based
around a two-tier approach and a three-tier approach, respectively. We call
these candidate architectures.
DEFINITION A candidate architecture for a system is a particular arrange-
ment of static and dynamic structures that has the potential to exhibit the
system’s required externally visible behaviors and quality properties.
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 19
Although the candidate architectures have different static and dynamic
structures, each must be able to meet the system’s overall requirements to
process airline bookings in a timely and efficient manner. However, although
all candidate architectures are believed to share the same important externally
visible behaviors (in this case, responses to booking transactions) and gen-
eral quality properties (such as acceptable response time, throughput, avail-
ability, and time to repair), they are likely to differ in the specific set of quality
properties that each exhibits (such as one being easier to maintain but more
expensive to build than another).
In each case, the extent to which the candidate actually exhibits these
behaviors and properties must be determined by further analysis of its static
and dynamic structures. For example, the two-tier candidate architecture
might meet the functional requirements better because it supports function-
ally richer clients; the three-tier candidate architecture might deliver better
throughput and response time because it is more loosely coupled.
It is part of the architect’s role to derive the static and dynamic structures
for each of the candidate architectures, understand the extent to which they
exhibit the required behaviors and quality properties, and select the best one.
Of course, what is meant by “best” may not always be clear; we will return to
this issue in Part II.
We can capture the relationship between the externally visible properties
of a system and its internal structure and organization as follows.
The externally visible behavior of a system (what it does) is determined
by the combined functional behavior of its internal elements.
The quality properties of a system (how it does it) such as performance,
scalability, and resilience arise from the quality properties of its internal
elements. (Typically, a system’s overall quality property is only as good
as the property of its worst-behaving or weakest internal element.)
Of course, it’s not really as simple as that! For example, a server that can-
not scale to process the workload submitted to it may also become function-
ally constrained (for example, users may not be able to log in to it or execute
some resource-heavy functions). However, we still find that this rather sim-
plistic distinction is a useful one that has informed much of our thinking.
The Importance of Software Architecture
Every computer system, large or small, is made up of pieces that are linked to-
gether. There may be a small number of these pieces, or perhaps only one, or
there may be dozens or hundreds; and this linkage may be trivial, or very
complicated, or somewhere in between.
20
 P ART I  A RCHITECTURE F UNDAMENTALS
Furthermore, every system is made up of pieces that interact with each
other and the outside world in a deterministic (predictable) way. Again, the
behavior may be simple and easily understood, or it may be so convoluted
that no one person can understand every aspect of it. However, this behavior
is still there and still (in theory at least) describable.
In other words, every system has an architecture, in the same way that
every building, bridge, and battleship has an architecture—and every human
body has a physiology.
This is such an important concept that we will state it formally as a princi-
ple here.
P RINCIPLE Every system has an architecture, whether or not it is docu-
mented and understood.
The architecture of a system is an intrinsic, fundamental property that is
present whether or not it has been documented and is understood. Every sys-
tem has precisely one architecture—although, as we will see, it can be repre-
sented in a number of ways.
A RCHITECTURALE LEMENTS
As explained previously, we standardize the term architectural element to re-
fer to the pieces from which systems are built.
DEFINITION An architectural element (or just element) is a fundamental
piece from which a system can be considered to be constructed.
The nature of an architectural element depends very much on the type of
system you are considering and the context within which you are considering
its elements. Programming libraries, subsystems, deployable software units
(e.g., Enterprise Java Beans or .NET assemblies), reusable software products
(e.g., database management systems), or entire applications may form architec-
tural elements in an information system, depending on the system being built.
An architectural element should possess the following key attributes:
A clearly defined set of responsibilities
A clearly defined boundary
A set of clearly defined interfaces, which define the services that the ele-
ment provides to the other architectural elements
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 21
Architectural elements are often known informally as components or
modules, but these terms are already widely used with established specific
meaning. In particular, the term component tends to suggest the use of a pro-
gramming-level component model (such as J2EE or .NET), while module tends
to suggest a programming language construct. Although these are valid archi-
tectural elements in some contexts, they won’t be the type of fundamental
system element used in others.
For this reason, we deliberately don’t use these terms from now on.
Instead, we use the term element throughout the book to avoid confusion (fol-
lowing the lead of others, including ISO 42010 and Bass, Clements, and
Kazman [BASS03]—see the Further Reading section at the end of this chapter
for more details).
STAKEHOLDERS
Traditional software development has been driven by the need of the deliv-
ered software to meet the requirements of users. Although the definition of
the term user varies, all software development methods are based around this
principle in one way or another.
However, the people affected by a software system are not limited to
those who use it. Software systems are not just used: They have to be built
and tested, they have to be operated, they may have to be repaired, they are
usually enhanced, and of course they have to be paid for. Each of these activ-
ities involves a number—possibly a significant number—of people in addition
to the users. Each of these groups of people has its own requirements, inter-
ests, and needs to be met by the software system.
We refer collectively to these people as stakeholders. Understanding the
role of the stakeholder is fundamental to understanding the role of the archi-
tect in the development of a software product or system. We define a stake-
holder as follows.
D EFINITION A stakeholder in the architecture of a system is an individual,
team, organization, or classes thereof, having an interest in the realization of
the system.
The definition is based on the one from ISO Standard 42010, which we
discuss in more depth in Part II. For now, let’s look at a couple of key concepts
from this definition.
22
 P ART I  A RCHITECTURE F UNDAMENTALS
Individual, Team, or Organization
First of all, consider the phrase “individual, team, or organization.” As we
shall see in this book, those with an interest in the architecture of a system
stretch far more widely than just its developers, or even its developers and us-
ers. A much broader community than this is affected by the realization of the
architecture as a system, such as those who have to support it, deploy it, or
pay for it.
Specifying the architecture is a key opportunity for the stakeholders to
direct its shape and direction. You will find, however, that some stakeholders
are more interested in their roles than others, for a variety of reasons that
have little to do with architecture. Part of your role, therefore, is to engage
and galvanize, to persuade people of the importance of their involvement, and
to obtain their commitment to the task.
As the definition notes, a stakeholder often represents a class of individ-
ual, such as user or developer, rather than a specific person. This presents
some problems because it may not be possible to capture and reconcile the
needs of all members of the class (all users, all developers) in the time avail-
able. Furthermore, you may not have the stakeholders at hand (e.g., when
developing a new product). In either case, you need to select some represen-
tative stakeholders who will speak for the group. We’ll come back to this in
Part II.
Interests and Concerns
Now consider the phrase “having an interest in the realization of the system.”
This criterion is—deliberately—a broad one, and its interpretation is entirely
specific to individual projects. As you will see when you start to develop your
architecture, you are engaged in a process of discovery as much as one of
capture—in other words, this early in the system development lifecycle, your
stakeholders may not yet know precisely what their requirements are.
Another way that we sometimes express this idea is to say that we are
interested in stakeholders who have concerns about the system. We find the
term concern particularly appropriate because of the broad range of possible
types of stakeholder involvement with a system.
DEFINITION A concern about an architecture is a requirement, an objective, a
constraint, an intention, or an aspiration a stakeholder has for that architecture.
Many concerns will be common among stakeholders, but some concerns
will be distinct and may even conflict. Resolving such conflicts in a way that
leaves stakeholders satisfied can be a significant challenge.
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 23
Expensive;
high quality;
longer time to market
High
Quality
More expensive;
moderate quality;
moderate time to
market
Low
Cost
Short Time
to Market
Inexpensive;
lower quality;
longer time to market
FIGURE 2–4 THE Q UALITY T RIANGLE
EXAMPLE Some of the important attributes of a software development
project are often shown as a triangle whose corners represent cost,
quality, and time to market. Ideally we would like a project to have high
quality, zero cost, and immediate delivery, but we know this isn’t possi-
ble. The quality triangle in Figure 2–4 shows that it is necessary to make
compromises between these three attributes, and the best you are likely
to achieve is two out of three. In this diagram, each apex of the triangle
represents one of these desired qualities, and we have shown a few
indicative combinations of the qualities on the diagram, to illustrate how
they affect each other.
For example, a high-quality system tends to take longer to build and
to cost more. Conversely, it is often possible to reduce the initial devel-
opment time, but, assuming costs are kept roughly constant, this comes
at the expense of reducing the quality of the delivered software.
One or more of these attributes is likely to be important to different
stakeholders, and it is the architect’s job to understand which of these
attributes is important to whom and to reach an acceptable compromise
when necessary. We’ll talk more about how to do this in Part II.
The Importance of Stakeholders
Stakeholders (explicitly or implicitly) drive the whole shape and direction of
the architecture, which is developed solely to create a system for their bene-
fit and to serve their needs. Stakeholders ultimately make or direct the fun-
damental decisions about scope, functionality, operational characteristics,
24
 P ART I  A RCHITECTURE F UNDAMENTALS
and structure of the eventual product or system—under the guidance of the
architect, of course. Without stakeholders, there would be no point in de-
veloping the architecture because there would be no need for the system it
will turn into, nor would there be anyone to build it, deploy it, run it, or pay
for it.
P RINCIPLE Architectures are created solely to meet stakeholder needs.
It follows that if a system does not adequately meet the needs of its stake-
holders, it cannot be considered a success—no matter how well it conforms to
good architectural practice. In other words, architectures must be evaluated
with respect to stakeholder needs as well as abstract architectural and soft-
ware engineering principles.
As we’ve seen, it is not uncommon for the needs of different stakeholders
to be in conflict with one another. There is no easy answer to such a dilemma,
and it often falls to the architect to strike an effective balance in such cases
(for example, by accepting higher maintenance costs in a performance-critical
system, caused by the level of optimization and integration of system ele-
ments required in order to reduce request processing latency).
P RINCIPLE A good architecture is one that successfully addresses the con-
cerns of its stakeholders and, when those concerns are in conflict, balances
them in a way that is acceptable to the stakeholders.
Part II explores the concept of stakeholders in more detail and explains
how they can be classified, identified, selected, and engaged in the develop-
ment of the architecture.
A RCHITECTURAL D ESCRIPTIONS
An architecture for a software system can be an incredibly complex thing. Part
of the architect’s role is to describe this complexity to the people who need to
understand it. The architect does this by means of an architectural description.
DEFINITION An architectural description (AD) is a set of products that
documents an architecture in a way its stakeholders can understand and dem-
onstrates that the architecture has met their concerns.
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 25
“Products” in this context consist of a range of things—particularly archi-
tectural models, but also scope definition, constraints, and principles. We dis-
cuss each of these in more detail in Parts II and III.
A description of an architecture has to present its essence and its detail at
the same time—in other words, it must provide an overall picture that sum-
marizes the whole system, but it also must decompose into enough detail that
it can be validated and the described system can be built.
Although it is true that every system has an architecture, it is unfortu-
nately not true that every system has an AD. Even if an architecture is
documented, it may be documented only in part, or the documentation may
be out-of-date or unused.
Strictly speaking, therefore, our definition describes a good AD. However,
an AD that its stakeholders cannot understand or that doesn’t demonstrate to
them that their concerns have been met is really not worth having—in fact, it
can be more of a liability than an asset. The AD needs to contain all of (and
ideally only) the information needed to communicate the architecture effec-
tively to those stakeholders who need to understand it.
P RINCIPLE Although every system has an architecture, not every system has
an architecture that is effectively communicated via an architectural description.
Of course, the chances of your architectural ideas being implemented as
you envisaged them are far less if the AD is inadequate.
EXAMPLE The AD for the airline reservation system referred to earlier fo-
cused strongly on the static structure (the key hardware and software ele-
ments and how they are organized) and to a lesser extent on its external
behavior (the way that those elements interact to respond to requests that
users could make). Because most users would have a customer at a sales
desk or on the end of a telephone, quick response time and system reliabil-
ity are paramount.
If the AD for such a system does not consider the quality properties of
the system in any detail—in particular, if there is no clear definition of
response-time requirements nor any performance models—it is quite likely
that when the system is deployed, it will deliver poor performance, particu-
larly under peak load.
The solution to this is to identify a group of users who can agree on
what the performance requirements are, and then the architect can balance
these against what analysis and testing reveal is practically possible.
26
 P ART I  A RCHITECTURE F UNDAMENTALS
This helps avoid the significant amount of enhancement and tuning inev-
itably required when performance problems emerge later in the lifecycle.
The architect writes the AD and is also one of its major users. You use the
AD as a memory aid, a basis for analysis, a record of decisions, and so on.
However, you are only one of the users of the AD. To a lesser or greater ex-
tent, all of the other stakeholders need to understand the architecture (or at
least parts of it) as it relates to them. If the AD does not help with this, it has
failed.
P RINCIPLE A good architectural description is one that effectively and con-
sistently communicates the key aspects of the architecture to the appropriate
stakeholders.
Nowadays there is a plethora of techniques, models, architecture descrip-
tion languages, and other ways to document architectures. Choosing the right
ones for a particular system development is a significant challenge in its own
right; you need to take into account the characteristics of the system and the
skills and capabilities of its stakeholders.
Part II explores the concept of ADs in more detail, and Parts III and IV
explain the different elements of an AD and how to create them.
R ELATIONSHIPS BETWEEN THE C ORE C ONCEPTS
The important relationships between our core concepts are illustrated in the
UML class diagram in Figure 2–5. The diagram brings out the following rela-
tionships among the concepts we have discussed so far.
A system is built to address the needs, concerns, goals, and objectives of
its stakeholders.
The architecture of a system comprises a number of architectural ele-
ments and their interelement relationships.
The architecture of a system can potentially be documented by an AD
(fully, partly, or not at all). In fact, there are many potential ADs for a
given architecture, some good, some bad.
An AD documents an architecture for its stakeholders and demonstrates
to them that it has met their needs.
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 27
Architectural
 relates
 Interelement
Element
 Relationship
2..n
 1..n
comprises
1..n
comprises
1..n
Architecture
has an
System
can be documented by
0..n
Architectural
Description
addresses the needs of
1..n
documents architecture for
Stakeholder
1..n
FIGURE 2–5 CORE C ONCEPT RELATIONSHIPS
We use standard UML conventions in Figure 2–5 and throughout the book.
Here, rectangles represent our architectural concepts, and directed lines represent
relationships from one concept to another. A filled diamond at the “from” end of a
line indicates an “is composed of” relationship. The cardinality of each relationship
(how many of one thing can be related to another) is shown at each end of each
line. The relationships are annotated to give a brief indication of what they mean.
SUMMARY
In this chapter we laid our foundations by defining and discussing some con-
cepts and terms we will be using throughout the rest of the book.
The architecture of a system defines its static structure, its dynamic
structure, its externally visible behavior, its quality properties, and the
principles that should guide its design and evolution . Each of these
aspects is important although not always addressed. Every computer
system has an architecture, even if we don’t understand it.
A candidate architecture for a system is one that has the potential to
exhibit the system’s required externally visible behaviors and quality
properties. Most problems have several candidate architectures, and it
is the job of the architect to select the best one.
An architectural element is a clearly identifiable, architecturally mean-
ingful piece of a system.
28
F URTHERP ART I  A RCHITECTURE F UNDAMENTALS
A stakeholder is a person, group, or entity with an interest in or concerns
about the realization of the architecture. Stakeholders include users but
also many other people, such as developers, operators, and acquirers.
Architectures are created solely to meet stakeholder needs.
An architectural description is a set of products that documents an archi-
tecture in a way its stakeholders can understand and demonstrates that
the architecture has met their concerns. Although every system has an
architecture, not every system has an effective AD.
R EADING
We have aligned our language and concepts in this chapter with the most re-
cent general standard we are aware of in the field of software architecture—
ISO/IEC Standard 42010 [ISO11] (an evolution of IEEE Standard 1471-2000
for architecture description). According to its own introduction, this standard
addresses “the creation, analysis and sustainment of architectures of systems
through the use of architecture descriptions.” Our conceptual model is based
on the one presented in the standard.
Much of our thinking on software architecture concepts is based on the
work done by the Software Architecture group of the Software Engineering
Institute. The book by Bass, Clements, and Kazman [BASS03] is a thorough
introduction to the main ideas in the field of software architecture and pro-
vides a lot more depth and background on the fundamental concepts than we
provide here.
One of the original books on software architecture is by Shaw and Garlan
[SHAW96]. This book provides a minimalist and elegant introduction to the
fundamental ideas in software architecture, including overviews of an AD, ar-
chitectural styles, and possible tool support. Even earlier than this, one of the
original papers in the software architecture field, by Perry and Wolf
[PERR92], is well worth reading for its clear focus on the important elements
of the discipline.
If you want to take a wider view of software architecture, a useful book
may be the cross-disciplinary Art of Systems Architecting [MAIE09]. This
book is novel in that it introduces and discusses the idea of architecture (and
“architecting”) as a set of principles and techniques valid across all complex
systems domains. A particular emphasis is placed on architecture heuristics,
and a set of interesting heuristics is provided. Examples are taken from build-
ings, manufacturing, social systems, IT, and collaborative systems.
A number of other good introductory books on the subject have appeared
in the years between the first and second editions of this book. In all of our
writing, we have tried to stress that it is important to focus your architecture
work on the most important aspects of the problem that you face, rather than
C HAPTER 2  S OFTWARE A RCHITECTURE C ONCEPTS
 29
trying to use every viewpoint and perspective in every case. George Fair-
banks’s book Just Enough Software Architecture [FAIR10] is a practical guide
to doing exactly this, showing you how to practice “risk-driven architecting”
in order to tailor your architecture work in response to the risks that you face.
Ian Gorton’s book Essential Software Architecture [GORT06] is a concise,
practical introduction to a number of important software architecture topics;
and Richard Taylor, Neno Medvidovic, and Eric Dashofy have created a very
comprehensive introduction to the subject in [TAYL09].
If you are interested in defining a formal process for your software archi-
tecture work, Peter Eeles and Peter Cripps’s book The Process of Software
Architecting [EELE09] will be a useful guide to achieving this.
This page intentionally left blank
3
V IEWPOINTSAND V IEWS
W
 hen system, you you start will the find daunting that you task have of designing some difficult the architecture architectural of ques-
 your
tions to answer.
What are the main functional elements of your architecture?
How will these elements interact with one another and with the outside
world?
What information will be managed, stored, and presented?
What physical hardware and software elements will be required to sup-
port these functional and information elements?
What operational features and capabilities will be provided?
What development, test, support, and training environments will be
provided?
A common temptation—one you should strongly avoid—is to try to answer
all of these questions by means of a single, heavily overloaded, all-encompassing
model. This sort of model (and we’ve all seen them) will probably use a mixture
of formal and informal notations to describe a number of aspects of the system
on one huge sheet of paper: the functional structure, software layering, concur-
rency, intercomponent communication, physical deployment environment, and
so on. Let’s see what happens when we try to use an all-encompassing model in
our AD, by means of an example.
As the example shows, this sort of AD is really the worst of all worlds.
Many writers on software architecture have pointed out that it simply isn’t pos-
sible to describe a software architecture by using a single model. Such a model
is hard to understand and is unlikely to clearly identify the archite cture’s most
31
32
 P ART I  A RCHITECTURE F UNDAMENTALS
EXAMPLE Although the airline reservation system we introduced in
Chapter 2 is conceptually fairly simple, in practice some aspects of this
system make it very complicated indeed.
The system’s data is distributed across a number of systems in different
physical locations.
A number of different types of data entry devices must be supported.
The system must be able to present some information in different
languages.
The system must be able to print tickets and other documents on a wide
range of printers.
The plethora of international regulations complicates the picture even
further.
After some discussion, the architect draws up a first-cut architecture for
the system, which attempts to represent all of its important aspects in a sin-
gle diagram. This model includes the full range of data entry devices (in-
cluding various dumb terminals, desktop PCs, and wireless devices), the
multiple physical systems on which data is stored or replicated data is
maintained, and some of the printing devices that must be supported (the
model does not cover remote printing because it is done at a separate facil-
ity). The model is heavily annotated with text to indicate, for example,
where multilanguage support is required and where data must be audited,
archived, or analyzed to support regulatory requirements.
However, no details of the network interfaces between the different
components are included—these are abstracted out into a network icon
because they are so complex. (In fact, the network design is probably
the most complicated aspect of the architecture, requiring support for a
number of different and largely incompatible network protocols, routing
over public and private networks, synchronous and asynchronous inter-
actions, and varying levels of service reliability and availability.) Fur-
thermore, the model does not address any of the implications of having
the same data distributed around multiple systems.
Because it is so complex and tries to address a wide mix of concerns in
the same diagram, the model fails to engage any of the stakeholders. The
users find it too complex and difficult to understand (particularly because of
the large number of physical hardware components represented). The tech-
nology stakeholders, on the other hand, tend to disregard it because of the
detail that is left out, such as the network topology. The legal team members
can’t use it to satisfy themselves that the regulatory aspects will be ade-
quately handled, and the sponsor finds it completely incomprehensible.
C HAPTER 3  V IEWPOINTS AND V IEWS
 33
Furthermore, the architect spends an inordinate amount of time keep-
ing it up-to-date—every time a new type of data entry device or printer is
discussed, for example, the diagram needs to be updated and reprinted on
a very large sheet of paper.
Because of these problems, the diagram soon becomes obsolete and is
eventually forgotten. Unfortunately, the issues that the model fails to
address do not disappear and thus cause many problems and delays
during the implementation and the early stages of live operation.
important features. It tends to poorly serve individual stakeholders because
they struggle to understand the aspects that interest them. Worst of all,
because of its complexity, a monolithic AD is often incomplete, incorrect, or
out-of-date.
P RINCIPLE It is not possible to capture the functional features and quality
properties of a complex system in a single comprehensible model that is un-
derstandable by, and of value to, its stakeholders.
We need to represent complex systems in a way that is manageable and
comprehensible by a range of business and technical stakeholders. A widely
used approach—the only successful one we have found—is to attack the
problem from different directions simultaneously. In this approach, the AD is
partitioned into a number of separate but interrelated views, each of which
describes a separate aspect of the architecture. Collectively, the views describe
the whole system.
To help you understand what we mean by a view, let’s consider the ex-
ample of an architectural drawing for one of the elevations of an office block.
This portrays the building from a particular aspect, typically a compass bear-
ing such as northeast. The drawing shows features of the building that are
visible from that vantage point but not from other directions. It doesn’t show
any details of the interior of the building (as seen by its occupants) or of its
internal systems (such as plumbing or air conditioning) that influence the en-
vironment its occupants will inhabit. Thus the blueprint is only a partial rep-
resentation of the building; you have to look at—and understand—the whole
set of blueprints to grasp the facilities and experience that the whole building
will provide.
Another way that a building architect might represent a new building
is to construct a scale model of it and its environs. This shows how the
building will look from all sides but again reveals nothing about the mech-
anisms to be used in its construction, its interior form, or its likely internal
environment.
34
 P ART I  A RCHITECTURE F UNDAMENTALS
S TRATEGY A complex system is much more effectively described by a set of
interrelated views, which collectively illustrate its functional features and
quality properties and demonstrate that it meets its goals, than by a single
overloaded model.
Let’s take a look at what this approach means for software architecture.
A RCHITECTURALV IEWS
An architectural view is a way to portray those aspects or elements of the ar-
chitecture that are relevant to the concerns the view intends to address—and,
by implication, the stakeholders to whom those concerns are important.
This idea is not new, going back at least as far as the work of David
Parnas in the 1970s and more recently Dewayne Perry and Alexander Wolf
in the early 1990s. However, it wasn’t until 1995 that Philippe Kruchten of
the Rational Corporation published his widely accepted written description
of views, Architectural Blueprints—The “4 + 1” View Model of Software
Architecture. This suggested four different views of a system and the use
of a set of scenarios (use cases) to elucidate its behavior. Kruchten’s ap-
proach has since evolved to form an important part of the Rational Unified
Process (RUP).
IEEE Standard 1471 (the predecessor of ISO Standard 42010) formalized
these concepts in 2000 and brought some welcome standardization of termi-
nology. In fact, our definition of a view is based on and extends the one from
the original IEEE standard.
DEFINITION A view is a representation of one or more structural aspects of
an architecture that illustrates how the architecture addresses one or more
concerns held by one or more of its stakeholders.
When deciding what to include in a view, ask yourself the following
questions.
 View scope: What structural aspects of the architecture are you trying
to represent? For example, are you trying to define the runtime func-
tional elements and their intercommunication, or the runtime environ-
ment and how the system is deployed into it? Do you need to
represent the dynamic or static elements of these structures? (For
example, in the case of the functional element structure, do you wish
C HAPTER 3  V IEWPOINTS AND V IEWS
 35
to show the elements and the connectors between them, or the se-
quence of interactions they perform in order to process an incoming
request, or both?)
 Element types: What type(s) of architectural element are you trying to
categorize? For example, when considering how the system is de-
ployed, do you need to represent individual server machines, or do
you just need to represent a service environment (like Force.com
SiteForce or Google AppEngine) that your system elements are
deployed into?
 Audience: What class(es) of stakeholder is the view aimed at? A view
may be narrowly focused on one class of stakeholder or even a specific
individual, or it may be aimed at a larger group whose members have
varying interests and levels of expertise.
 Audience expertise: How much technical understanding do these
stakeholders have? Acquirers and users, for example, will be experts
in their subject areas but are unlikely to know much about hardware
or software, while the converse may apply to developers or support
staff.
 Scope of concerns: What stakeholder concerns is the view intended to
address? How much do the stakeholders know about the architectural
context and background to these concerns?
 Level of detail: How much do these stakeholders need to know about this
aspect of the architecture? For nontechnical stakeholders such as users,
how competent are they in understanding its technical details?
As with the AD itself, one of your main challenges is to get the right con-
tent into your views. Provide too much irrelevant detail, for example, and
your audience will be overwhelmed; too little information, and you risk your
audience being confused or making assumptions that may not be valid. There
are two key questions you should ask yourself when deciding what to include
in a view. First of all, can the stakeholders that it targets use it to determine
whether their concerns have been met? And second, can those stakeholders
use it to successfully undertake their role in building the system?
We will explore the second question in more detail in Chapter 9, but for
now we will summarize these questions as follows.
S TRATEGY Only include in a view information that furthers the objectives of
your AD—that is, information that helps explain the architecture to stake-
holders or demonstrates that the goals of the system (i.e., the concerns of its
stakeholders) are being met.
36
V IEWPOINTS
P ART I  A RCHITECTURE F UNDAMENTALS
It would be hard work if every time you were creating a view of your architec-
ture you had to go back to first principles to define what should go into it.
Fortunately, you don’t quite have to do that.
In his introductory paper, Philippe Kruchten defined four standard views,
namely, Logical, Process, Physical, and Development. The IEEE standard
made this idea generic (and did not specify one set of views or another) by
proposing the concept of a viewpoint.
The objective of the viewpoint concept is an ambitious one—no less
than making available a library of templates and patterns that can be used
off the shelf to guide the creation of an architectural view that can be
inserted into an AD. We define a viewpoint (again after IEEE Standard
1471) as follows.
DEFINITION A viewpoint is a collection of patterns, templates, and conven-
tions for constructing one type of view. It defines the stakeholders whose
concerns are reflected in the viewpoint and the guidelines, principles, and
template models for constructing its views.
Architectural viewpoints provide a framework for capturing reusable
architectural knowledge that can be used to guide the creation of a particular
type of (partial) AD. You may find it helpful to compare the relationship
between viewpoints and views to the relationship between classes and
objects in object-oriented development.
A class definition provides a template for the construction of an object.
An object-oriented system will include at runtime a number of objects,
each of a specified class.
A viewpoint provides a template for the construction of a view. A viewpoints-
and-views-based architecture definition will include a number of views, each
conforming to a specific viewpoint.
Viewpoints are an important way of bringing much-needed structure and
consistency to what was in the past a fairly unstructured activity. By defining
a standard approach, a standard language, and even a standard metamodel
for describing different aspects of a system, stakeholders can understand any
AD that conforms to these standards once familiar with them.
In practice, of course, we haven’t fully achieved this goal yet. There are
no universally accepted ways to model software architectures, and many
ADs use their own homegrown conventions (or even worse, no particular
conventions at all). However, the widespread acceptance of techniques such
C HAPTER 3  V IEWPOINTS AND V IEWS
 37
as entity-relationship models and of modeling languages such as UML takes
us some way toward this goal.
In any case, it is extremely useful to be able to categorize views according
to the types of concerns and architectural elements they present.
S TRATEGY When developing a view, whether or not you use a formally
defined viewpoint, be clear in your own mind what sorts of concerns the view
is addressing, what types of architectural elements it presents, and who the
viewpoint is aimed at. Make sure that your stakeholders understand these as
well.
R ELATIONSHIPS BETWEEN THE C ORE C ONCEPTS
To put views and viewpoints in context, we can now extend the conceptual
model we introduced in Chapter 2 to illustrate how views and viewpoints con-
tribute to the overall picture (see Figure 3–1).
Architectural
 relates
 Interelement
Element
 Relationship
2..n
 1..n
comprises
1..n
comprises
1..n
Architecture
has an
System
can be documented by
 addresses the needs of
0..n
 1..n
Architectural
 documents architecture for
Stakeholder
Description
1..n
1..n
comprises
 has
1..n
 1..n
conforms to
 addresses
View
 Viewpoint
 Concern
0..n
 1..n
 1..n
FIGURE 3–1 V IEWS AND V IEWPOINTS IN C ONTEXT
38
T HEP ART I  A RCHITECTURE F UNDAMENTALS
We have added the following relationships to the diagram we originally
presented as Figure 2–5.
A viewpoint defines the aims, intended audience, and content of a
class of views and defines the concerns that views of this class will
address.
A view conforms to a viewpoint and so communicates the resolution of a
number of concerns (and a resolution of a concern may be communicated
in a number of views).
An AD comprises a number of views.
B ENEFITS OF USING V IEWPOINTS AND VIEWS
Using views and viewpoints to describe the architecture of a system benefits
the architecture definition process in a number of ways.
Separation of concerns: Describing many aspects of the system via a single
representation can cloud communication and, more seriously, can result in
independent aspects of the system becoming intertwined in the model. Sep-
arating different models of a system into distinct (but related) descriptions
helps the design, analysis, and communication processes by allowing you to
focus on each aspect separately.
Communication with stakeholder groups: The concerns of each stake-
holder group are typically quite different (e.g., contrast the primary con-
cerns of end users, security auditors, and help-desk staff), and
communicating effectively with the various stakeholder groups is quite a
challenge. The viewpoint-oriented approach can help considerably with
this problem. Different stakeholder groups can be guided quickly to dif-
ferent parts of the AD based on their particular concerns, and each view
can be presented using language and notation appropriate to the knowl-
edge, expertise, and concerns of the intended readership.
Management of complexity: Dealing simultaneously with all of the aspects
of a large system can result in overwhelming complexity that no one person
can possibly handle. By treating each significant aspect of a system sepa-
rately, the architect can focus on each in turn and so help conquer the com-
plexity resulting from their combination.
Improved developer focus: The AD is of course particularly important for the
developers because they use it as the foundation of the system design. By
separating out into different views those aspects of the system that are par-
ticularly important to the development team, you help ensure that the right
system gets built.
V IEWPOINTOURC HAPTER 3  V IEWPOINTS AND V IEWS
 39
P ITFALLS
Of course, the use of views and viewpoints won’t solve all of your software archi-
tecture problems automatically. Although we have found that using views is really
the only way to make the problem manageable, you need to be aware of some pos-
sible pitfalls when using the view-and-viewpoint-based approach.
Inconsistency: Using a number of views to describe a system inevitably
brings consistency problems. It is theoretically possible to use architec-
ture description languages to create the models in your views and then
cross-check these automatically (much as graphical modeling tools
attempt to check structured or object-oriented methods models), but
there are no such machine-checkable architecture description languages
in widespread use today. This means that achieving cross-view consis-
tency within an AD is an inherently manual process. To assist with this,
Chapter 23 includes a checklist to help you ensure consistency between
the standard viewpoints presented in our catalog in Part III.
Selection of the wrong set of views: It is not always obvious which set of
views is suitable for describing a particular system. This is influenced by a
number of factors, such as the nature and complexity of the architecture, the
skills and experience of the stakeholders (and of the architect), and the time
available to produce the AD. There really isn’t an easy answer to this prob-
lem, other than your own experience and skill and an analysis of the most
important concerns that affect your architecture.
Fragmentation: Having several views of your architecture can make
the AD difficult to understand. Each separate view also involves a sig-
nificant amount of effort to create and maintain. To avoid fragmenta-
tion and minimize the overhead of maintaining unnecessary
descriptions, you should eliminate views that do not address signifi-
cant concerns for the system you are building. In some cases, you may
also consider creating hybrid views that combine models from a num-
ber of views in the viewpoint set (e.g., creating a combined deploy-
ment and concurrency view). Beware, however, of the combined views
becoming difficult to understand and maintain because they address a
combination of concerns.
V IEWPOINT C ATALOG
Part III of this book presents our catalog of seven core viewpoints for information
systems architecture: the Context, Functional, Information, Concurrency, Devel-
opment, Deployment, and Operational viewpoints. Although the viewpoints are
(largely) disjoint, we find it convenient to group them as shown in Figure 3–2.
40
P ART I  A RCHITECTURE F UNDAMENTALS
Context Viewpoint
Functional Viewpoint
Information Viewpoint
Development Viewpoint
Deployment Viewpoint
Concurrency Viewpoint
 Operational Viewpoint
FIGURE 3–2 V IEWPOINT G ROUPINGS
The Context viewpoint describes the relationships, dependencies, and
interactions between the system and its environment (the people, sys-
tems, and external entities with which it interacts).
The Functional, Information, and Concurrency viewpoints characterize
the fundamental organization of the system.
The Development viewpoint exists to support the system’s construction.
The Deployment and Operational viewpoints characterize the system
once in its live environment.
You can use the shape and position of the icons in Figure 3–2 to help un-
derstand how our viewpoints are related to one another. We have put the
Context viewpoint at the top of the diagram to indicate its role as the “over-
arching” viewpoint that informs the scope and content of all the others. We
group the Functional, Information, and Concurrency viewpoints together at
the left, to highlight that between them they define how the system provides
its functionality.
The viewpoints on the right-hand side are to some extent driven by those
on the left; for example, the Development viewpoint defines standards and
models for the construction of the architecture’s functional, information, and
concurrency elements. We have further grouped the Deployment and Opera-
tional viewpoints, since between them, these views define the system’s produc-
tion environment.
C HAPTER 3  V IEWPOINTS AND V IEWS
 41
Viewpoint Overview
Table 3–1 briefly describes our viewpoints.
Of course, not all of these viewpoints may apply to your architecture, and
some will be more important than others. You may not need views of all of
these types in your AD, and in some cases there may be other viewpoints that
you need to identify and add yourself. This means that your first job is to un-
derstand the nature of your architecture, the skills and experience of the
stakeholders, and the time available and other constraints, and then to come
up with an appropriate selection of views.
FIGURE 3–2 VIEWPOINTViewpoint
Context
Functional
Information
Concurrency
Development
C ATALOG
Definition
Describes the relationships, dependencies, and interactions between the
system and its environment (the people, systems, and external entities
with which it interacts). The Context view will be of interest to many of
the system’s stakeholders and plays an important role in helping them to
understand its responsibilities and how it relates to their organization.
Describes the system’s runtime functional elements, their responsibilities,
interfaces, and primary interactions. A Functional view is the cornerstone
of most ADs and is often the first part of the description that stakeholders
try to read. It drives the shape of other system structures such as the infor-
mation structure, concurrency structure, deployment structure, and so on.
It also has a significant impact on the system’s quality properties such as
its ability to change, its ability to be secured, and its runtime performance.
Describes the way that the system stores, manipulates, manages, and dis-
tributes information. The ultimate purpose of virtually any computer sys-
tem is to manipulate information in some form, and this viewpoint develops
a complete but high-level view of static data structure and information flow.
The objective of this analysis is to answer the big questions around content,
structure, ownership, latency, references, and data migration.
Describes the concurrency structure of the system and maps functional
elements to concurrency units to clearly identify the parts of the system
that can execute concurrently and how this is coordinated and con-
trolled. This entails the creation of models that show the process and
thread structures that the system will use and the interprocess commu-
nication mechanisms used to coordinate their operation.
Describes the architecture that supports the software development pro-
cess. Development views communicate the aspects of the architecture
of interest to those stakeholders involved in building, testing, main-
taining, and enhancing the system.
Continued on next page
42
 P ART I  A RCHITECTURE F UNDAMENTALS
FIGURE 3–2 V IEWPOINT C ATALOG (CONTINUED)
Viewpoint
Deployment
Operational
Definition
Describes the environment into which the system will be deployed and
the dependencies that the system has on elements of it. This view cap-
tures the hardware environment that your system needs (primarily the
processing nodes, network interconnections, and disk storage facilities
required), the technical environment requirements for each element,
and the mapping of the software elements to the runtime environment
that will execute them.
Describes how the system will be operated, administered, and sup-
ported when it is running in its production environment. For all but the
simplest systems, installing, managing, and operating the system is a
significant task that must be considered and planned at design time.
The aim of the Operational viewpoint is to identify system-wide strate-
gies for addressing the operational concerns of the system’s stakehold-
ers and to identify solutions that address these.
While it can be hard to generalize, and it is important to choose your set
of views for the specific context in which you find yourself, Table 3–2 lists the
relative importance that we have often found each view to have for some typ-
ical types of information systems. We suggest you use this table as a starting
point when choosing the views to include in your AD.
TABLE 3–2 M OST I MPORTANT V IEWS FOR T YPICAL S YSTEM TYPES
OLTP
Information
 Calculation Service/ DSS/MIS
 High-Volume Enterprise
System
 Middleware
 System
 Web Site
 Package
Context
 High
 Low
 High
 Medium
 Medium
Functional
 High
 High
 Low
 High
 High
Information
 Medium
 Low
 High
 Medium
 Medium
Concurrency
 Low
 High
 Low
 Medium
 Varies
Development
 High
 High
 Low
 High
 High
Deployment
 High
 High
 High
 High
 High
Operational
 Varies
 Low
 Medium
 Medium
 High
C HAPTER 3  V IEWPOINTS AND V IEWS
 43
SUMMARY
Capturing the essence and the detail of the whole architecture in a single model
is just not possible for anything other than simple systems. If you try to do this,
you will end up with a Frankenstein monster of a model that is unmanageable
and does not adequately represent the system to you or any of the stakeholders.
By far the best way of managing this complexity is to produce a number
of different representations of all or part of the architecture, each of which
focuses on certain aspects of the system, showing how it addresses some of
the stakeholder concerns. We call these views.
To help you decide what views to produce and what should go into any
particular view, you use viewpoints, which are standardized definitions of
view concepts, content, and activities.
The use of views and viewpoints brings many benefits, such as separa-
tion of concerns, improved communication with stakeholders, and manage-
ment of complexity. However, it is not without its pitfalls, such as
inconsistency and fragmentation, and you must be careful to manage these.
In this chapter, we introduced our viewpoint catalog, comprising the Con-
text, Functional, Information, Concurrency, Development, Deployment, and
Operational viewpoints, which we describe in detail in Part III.
FURTHER R EADING
A lot of useful guidance on creating ADs using views (including a discussion
of when and how to combine views) and thorough guidance for creating the
documentation for a wide variety of types of views can be found in Clements
et al. [CLEM10]. Other references that help to make sense of viewpoints and
views are IEEE Standard 1471 [IEEE00], ISO Standard 42010 [ISO11], and
Kruchten’s “4 + 1” approach [KRUC95]. One of the earliest explicit references
to the need for architectural views appears in Perry and Wolf [PERR92].
Some of the other viewpoint taxonomies that have been developed over
the last decade or so—including Kruchten’s “4 + 1,” RM-ODP, the viewpoint
set by Hofmeister et al. [HOFM00], and the set by Garland and Anthony
[GARL03]—are described in the Appendix, together with recommendations
for further reading in this area.
Part III, where we describe our viewpoint catalog in detail, contains refer-
ences for specific view-related reading.
This page intentionally left blank
4
A RCHITECTURAL
P ERSPECTIVES
I
 n tional, Chapter Information, 3, we explained and Deployment how we use viewpoints) viewpoints to (such guide asthe the process Context, of Func-
 cap-
turing and representing the architecture as a set of views, with the development
of each view being guided by the use of a specific viewpoint. When creating a
view, your focus is on the issues, concerns, and solutions pertinent to that view.
So, for an Information view, for example, you focus on things such as informa-
tion structure, ownership, transactional integrity, data quality, and timeliness.
Many of the important concerns that are pertinent to one view are much
less important when considering the others. Data ownership, for example, is
not key to formulating the Concurrency view, nor is the development environ-
ment a major concern when considering the Functional view. (Of course, the
decisions taken in one view can have a considerable impact on the others, and
it is a big part of the architect’s job to make sure that these implications are
understood. However, the concerns addressed in different views are largely
different.)
Although the views, when combined, form a representation of the whole
architecture, we can consider them largely independent of one another—a dis-
joint partition of the whole architectural analysis. In fact, for any significant
system, you usually must partition your analysis this way because the entire
problem is too much to understand or describe in a single piece.
QUALITY PROPERTIES
Many architectural decisions address concerns that are common to many or
all views. These concerns are normally driven by the need for the system
to exhibit a certain quality property rather than to provide a particular
45
46
 P ART I  A RCHITECTURE F UNDAMENTALS
function. In our experience, trying to address these aspects of an architec-
ture by using viewpoints doesn’t work well. Let’s look at an example to
understand why.
EXAMPLE Security is clearly a vital quality of most systems. It has
always been important to be able to restrict access to data or functional-
ity to appropriate classes of users, and in the age of the Internet, good
external and internal security is even more important. If some of your
systems are exposed to the wider world, they are vulnerable to attack,
and the consequences of a breach can be disastrous for finances or pub-
lic relations. (The large number of high-profile Internet security failures
in Europe and North America that have occurred since the early part of
the millennium illustrates this clearly.)
In our experience, security is often not thought through properly early
in the project lifecycle. Part of the reason for this is that security is hard—
the means for achieving an appropriate level of security are complex and
require sophisticated analysis. Also, it may be considered to be “someone
else’s problem”—the responsibility of a specialist security group rather
than of the organization as a whole. You may be surprised, therefore, that
we have not included a Security viewpoint in our catalog to go along with
the others (Functional, Information, Deployment, and so forth).
We used to approach concerns such as security just like that our-
selves. We used a Security viewpoint and started to consider which
classes of stakeholders have concerns in this area, what this viewpoint
should consist of, and how a typical Security view might actually look.
However, experience taught us that security is an important factor
that affects aspects of the architecture addressed by most if not all of the
other viewpoints we presented in Chapter 3. Furthermore, which of the
system’s security qualities are significant depends on which viewpoint
we are considering. Here are some examples.
From the Functional viewpoint, the system needs the ability to
identify and authenticate its users (internal and external, human
and mechanical). Security processes should be effective but unob-
trusive, and any external processes exposed to the outside world
need to be resilient to attack.
From the Information viewpoint, the system must be able to con-
trol different classes of access to information (read, insert, update,
delete). The system may need to apply these controls at varying
levels of granularity (e.g., defining object-level security within a
database).
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 47
From the Operational viewpoint, the system must be able to main-
tain and distribute secret information (e.g., keys and passwords)
and must be up-to-date with the latest security updates and
patches.
When we consider the system from the Development, Concurrency,
and Deployment viewpoints, we’ll probably also find aspects of the
architecture that will be affected by security needs.
So our overall criterion of “the system must be secure” actually breaks
down across the viewpoints into a number of more specific criteria.
As the example shows, there is an inherent need to consider quality prop-
erties such as security in each architectural view. Considering a quality property
in isolation just doesn’t make sense, so using a viewpoint to guide the creation
of another view for each quality property doesn’t make sense either.
A RCHITECTURALP ERSPECTIVES
Going back to our example, although security is clearly important, represent-
ing it in our conceptual model of software architecture as another viewpoint
doesn’t really work. A comprehensive security viewpoint would have to con-
sider process security, information security, operational security, deployment
security, and so on. In other words, it would affect exactly the aspects of the
system that we have considered so far using our viewpoints.
Rather than defining another viewpoint and creating another view, we need
some way to modify and enhance our existing views to ensure that our architec-
ture exhibits the desired quality properties. This should define the activities that
we would perform to determine whether the architecture exhibits the required
quality properties, some proven architectural tactics that we would apply to
improve the architecture if we discover that it doesn’t, and some guidelines we
would follow to help us apply these tactics in the right way.
We therefore need something in our conceptual model that can be consid-
ered “orthogonal” to viewpoints, and we have coined the term architectural
perspective (which we shorten to perspective) to refer to it.
D EFINITION An architectural perspective is a collection of architectural
activities, tactics, and guidelines that are used to ensure that a system exhib-
its a particular set of related quality properties that require consideration
across a number of the system’s architectural views.
48
 P ART I  A RCHITECTURE F UNDAMENTALS
Although our use of the term perspective is relatively new compared to the
other concepts we discuss in the book, the ideas behind it have a very established
pedigree. The issues addressed by perspectives are often referred to as cross-
cutting concerns or nonfunctional requirements of the architecture, although we
prefer not to use this latter term. 1
With perspectives, we are trying to systematize what a good architect does
anyway—understand the quality properties that are required; assess and review
the architectural models to ensure that the architecture exhibits the required prop-
erties; identify, prototype, test, and select architectural tactics to address cases
when the architecture is lacking; and so on.
DEFINITION An architectural tactic is an established and proven approach
you can use to help achieve a particular quality property.
An example architectural tactic for achieving satisfactory overall system
performance might be to define different processing priorities for different
parts of the system’s workload, and to manage this by using a priority-based
process scheduler. The concept of architectural tactics was created and devel-
oped by the software architecture researchers at the Carnegie Mellon Software
Engineering Institute (SEI), and although our definition is worded slightly
differently from theirs, our approach to tactics is based directly on their work
in this area.
Don’t confuse tactics with design patterns, which we discuss in Part II.
Although tactics and patterns are both valuable sources of design knowledge,
a tactic is much more general and less constraining than a classical design
pattern because it does not mandate a particular software structure but pro-
vides general guidance on how to design a particular aspect of your system.
(See the Further Reading section at the end of this chapter for some refer-
ences on tactics.)
A perspective provides a framework to guide and formalize this process.
This means that you never work with perspectives in isolation but instead use
them with each view of your architecture to analyze and validate its qualities
and to drive further architectural decision making. We describe this as apply-
ing the perspective to the view.
1. Although it is true that the perspectives tend to address concerns that are dis-
tinct from what the system actually does, the division of concerns as functional or
nonfunctional is often quite artificial, and we try to avoid the use of these terms.
Perspectives can have an impact on how a system works, sometimes significantly,
and using these terms can imply that these areas are somehow less important than
functionality.
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 49
EXAMPLE The ability to identify and authenticate users is a key quality
property of almost every software system. It is very important to be able
to confirm that users really are who they claim to be and validate that
they are allowed to access the system.
To meet this requirement, the architecture therefore needs sound
mechanisms to identify and authenticate its users. These features mani-
fest themselves (to a greater or lesser extent) in different architectural
views; for example:
The system needs access to an authentication service or to a list of
users and their passwords or other authentication data. If authen-
tication data is held within the application, the data must be held
in such a way that it cannot be easily obtained by others (e.g., one-
way encrypted passwords). Access to an external authentication
service would be shown in the Context and Functional views (and
possibly the Deployment view); if authentication information
needs to be held securely within the system, this would be defined
in the Information view.
The system must protect access by means of login screens of some
sort, which would require the user to present appropriate credentials
before being allowed to access the system. It also requires the ability
for operational staff to manage the list of users and to reset their pass-
words. The functional features would be defined in the Functional
view and the operational aspects defined in the Operational view.
In some application domains, the system might need to maintain a
verifiably secure store of security keys and certificates, using spe-
cialized hardware in a secure physical environment. These features
would be defined in the Deployment view.
Different quality properties, such as security, performance, availability, or
usability, vary in their applicability to different types of systems. Usability,
for example, is unlikely to be particularly important to an infrastructure
project with little or no functionality exposed to users. However, broad cate-
gories of systems are likely to have similar overall quality property require-
ments and common ways of meeting them, so we intend perspectives to be
defined in sets, with each set aimed at a particular category of system. In this
book we focus on large-scale information systems and have therefore defined
a set of perspectives for systems in that domain.
In our experience, the most important perspectives for large information sys-
tems include Security (ensuring controlled access to sensitive system resources),
Performance and Scalability (meeting the system’s required performance profile
50
 P ART I  A RCHITECTURE F UNDAMENTALS
and handling increasing workloads satisfactorily), Availability and Resilience
(ensuring system availability when required and coping with failures that could
affect this), and Evolution (ensuring that the system can cope with likely
changes). We define these perspectives in detail in Part IV, along with a number
of less widely applicable perspectives such as Regulation (the ability of the sys-
tem to conform to local and international laws, quasi-legal regulations, company
policies, and other rules and standards).
You will find these perspective definitions useful whether you are just
starting out as an architect or already have significant experience in the role.
You can use the definitions in a number of different ways.
A perspective is a useful store of knowledge, helping you quickly review
your architectural models for a particular quality property without having
to absorb a large quantity of more detailed material.
A perspective acts as an effective guide when you are working in an area
that is new to you and you are not familiar with its typical concerns,
problems, and solutions.
A perspective is a useful memory aid when you are working in an area
that you are more familiar with, to make sure that you don’t forget any-
thing important.
In general, you should try to apply your perspectives, even if only infor-
mally, as early as possible in the design of your architecture. This will help
prevent you from going down architectural blind alleys in which you develop
a model that is functionally correct but offers, for example, poor performance
or availability.
As with viewpoints, it is important to define perspectives in a standard
way, to make them easy to use and to ensure that they all approach a subject
area in the same general way. The perspective definitions in Part IV are all
structured in the following manner.
Applicability: This section explains which of your views are most likely to
be affected by applying the perspective. For example, applying the Evolu-
tion perspective might affect your Functional view more than your Oper-
ational view.
Concerns: This information defines the quality properties that the per-
spective addresses.
Activities: In this section, we explain the steps for applying the perspec-
tive to your views—identifying the important quality properties, analyz-
ing the views against these properties, and then making architectural
design decisions that modify and improve the views.
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 51
Architectural tactics: Each perspective identifies and describes the most
important tactics for achieving its quality properties.
Problems and pitfalls: This section explains the most common things that
can go wrong and gives guidance on how to recognize and avoid them.
Checklists: The checklists provide a list of questions to help you make
sure you have addressed the most important concerns, considered the
most appropriate tactics, and avoided the most common pitfalls.
Further reading: Our perspective descriptions are necessarily brief, help-
ing you understand the most important issues, problems, and proven
practices. The Further Reading section provides a number of pointers to
further information.
A PPLYINGPERSPECTIVES TO V IEWS
As we indicate in Figure 4–1, you apply each relevant perspective to some or all
of the views that you are using in order to address that perspective’s system-
wide quality property concerns. The architectural views contain the description of
the architecture, while the perspectives guide you through the process of analyz-
ing and modifying your architecture to make sure it exhibits a particular quality
property.
Although every perspective can be applied to every view (in other words,
the relationship between perspectives and views is many-to-many), in prac-
tice, because of time constraints and the risks that you need to address, you
usually apply only some of the perspectives to some of the views. An easy way
to understand this process is to think of a two-dimensional grid, with views
along one axis and perspectives along another, as shown in Figure 4–2.
Each rectangle in the grid represents the application of a perspective to a
view, and the contents of the rectangle define the important qualities and con-
cerns at that intersection. Here are some examples.
When you apply the Security perspective to the Information view, it
guides the design of your architecture so that, for example, it includes
appropriate data access control and data ownership.
When you apply the Performance perspective to the Concurrency view, it
guides the design of your architecture so that, for example, a suitable pro-
cess structure is used, and shared resources will not lead to contention.
When you apply the Evolution perspective to the Functional view, it
guides the design of your architecture so that, for example, you consider
the types of changes that will be required and build in the right level of
flexibility.
52
P ART IA RCHITECTURE F UNDAMENTALS
Security Perspective
Performance Perspective
Availability Perspective
Usability Perspective
Accessibility Perspective
Location Perspective
Regulation Perspective
etc.
Context View
Functional View
 Development View
Information View
 Deployment View
Concurrency View
 Operational View
FIGURE 4–1 A PPLYING P ERSPECTIVES TO V IEWS
You can draw a grid like the one shown in Figure 4–2 to record which
perspectives you intend to apply to which views. When you are working on
a particular view, look along the rows of the grid to remind yourself of the
important non-view-specific qualities and how they manifest themselves in
that view. You may even want to add detail to your grid to record how
important each perspective is to each view for your system, as illustrated in
Table 4–1.
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 53
PERSPECTIVES
Security
 Performance
 Availability
 Evolution
l
anoitarepOS
WEIVy
cnerrucnoCn
oitamrofnIl
anoitcnuFConcurrency
Performance
(shared resources,
blocking, queuing,
coordination)
Information
Security
(access control,
access classes,
object-level
security)
Functional
Evolution
(extension points, flexible
interfaces, meta-
approaches)
FIGURE 4–2 EXAMPLES OF APPLYING PERSPECTIVES TO VIEWS
EXAMPLE Going back to our example of security, having decided on a
candidate architecture for your system and captured it as a set of views,
you would then apply the Security perspective in order to ensure that the
system meets its security requirements.
To apply this perspective, you would perform a number of activities,
as listed in the perspective’s definition, such as identifying the sensitive
resources in the system, identifying the threats that the system faces,
and deciding how to mitigate each threat by using suitable security pro-
cesses and technology. The result would typically be some changes to
your candidate architecture such as those listed here.
54
 P ART I  A RCHITECTURE F UNDAMENTALS
You might decide to partition the system differently in order to
easily restrict access to parts of it. This would affect your Func-
tional view.
Your security design might introduce new hardware and software
elements to the system to limit access or to add additional guaran-
tees (such as encryption to ensure privacy). You would need to add
these new elements to your Deployment view to define where they
fit, and you might need to update the Development view to define
how these new elements should be used.
You might identify new operational procedures to support secure
operation (e.g., certificate management) or modify existing proce-
dures to ensure security (e.g., handling backups of sensitive
data). These procedural changes will modify the Operational
view.
Applying the Security perspective has not resulted in a new security
view but has identified a number of modifications to your existing views
that help address your stakeholders’ security concerns.
TABLE 4–1 T YPICAL V IEW AND PERSPECTIVE A PPLICABILITY
Perspectives
Performance and
 Availability and
Views
 Security
 Scalability
 Resilience
 Evolution
Context
 Medium
 Low
 Low
 Medium
Functional
 Medium
 Medium
 Low
 High
Information
 Medium
 Medium
 Low
 High
Concurrency
 Low
 High
 Medium
 Medium
Development
 Medium
 Low
 Low
 High
Deployment
 High
 High
 High
 Low
Operational
 Medium
 Low
 Medium
 Low
C ONSEQUENCES OF A PPLYING A P ERSPECTIVE
Applying a perspective to a view can lead to insights, improvements, and artifacts.
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 55
Insights
Applying a perspective almost always leads to the creation of something—usually
some sort of model—that provides an insight into the system’s ability to meet a
required quality property. Such a model demonstrates either that the architecture
meets its required quality properties or (more likely in the early stages of architec-
ture definition) that it is deficient in some way.
EXAMPLE Applying the Security perspective might reveal the existence
of a number of significant security threats that are not countered by the
system in its current form. You would then need to understand these
threats, understand what the risks are, and understand the impact these
risks have on your architecture.
These insights normally drive further architectural design activity and
are usefully recorded in their own right as rationales for significant design
decisions.
Improvements
If applying the perspective tells you that the architecture will not meet one of its
quality properties, the architecture needs to be improved. In this case, you may
need to change an existing model in the view, create additional models to fur-
ther develop the content of the view, or perhaps do both of these.
EXAMPLE Applying the Performance and Scalability perspective to your
Deployment view might demonstrate that you need to replicate the applica-
tion servers in order to be capable of scaling to meet expected demand.
This could lead to a change to the Deployment model to show several serv-
ers instead of one and possible changes to the Functional or Information
views to support this load balancing.
These improvements are, of course, integral to the AD and should be
given as much prominence as your original models.
56
 P ART I  A RCHITECTURE F UNDAMENTALS
Artifacts
Some of the models and other deliverables created as a result of applying a
perspective will be of only passing interest and will probably be discarded
once the insight or improvement they reveal is understood. However, other
outputs of applying a perspective are of significant lasting value and are im-
portant supporting architectural information. These outputs, which we term
artifacts, are a valuable outcome of applying a perspective and should be
preserved.
EXAMPLE Applying the Location perspective to your Deployment
view might result in a spreadsheet that models the physical network to
show that there is sufficient bandwidth and capacity for the expected
traffic. This spreadsheet is a useful artifact that is likely to be needed
in the future to investigate the probable impact of changes to the sys-
tem or the network. You should retain and reference this artifact from
the AD.
Artifacts are typically captured as documents, models, or implementa-
tions, which are referenced from the AD as supporting information. Small
documents can be integrated into the AD as appendices, but take care to avoid
creating a huge document because this can become unwieldy and difficult to
read and maintain.
R ELATIONSHIPS BETWEEN THE C ORE C ONCEPTS
To put perspectives in context, we can now add a further piece to our concep-
tual model, as shown in Figure 4–3.
We have added the following relationships to update the similar diagram
we showed previously as Figure 3–1.
The content of a view can be shaped by a number of perspectives, in order
to ensure the system’s ability to exhibit the quality properties considered
by that perspective.
A perspective addresses a number of concerns of the system’s stakeholders.
T HE B ENEFITS OF USING P ERSPECTIVES
Applying perspectives to a view benefits your AD in several ways.
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 57
Architectural
 relates
 Interelement
Element
 Relationship
2..n
 1..n
1..n
comprises
comprises
1..n
Architecture
has an
System
can be documented by
 addresses the needs of
0..n
 1..n
Architectural
 documents architecture for
Stakeholder
Description
1..n
1..n
comprises
 has
1..n
 1..n
conforms to
 addresses
View
 Viewpoint
 Concern
0..n
 1..n
 1..n
0..n
1..n
shaped by
0..n
1..n
Perspective
 addresses
FIGURE 4–3 P ERSPECTIVES IN C ONTEXT
The perspective defines concerns that guide architectural decision mak-
ing to help ensure that the resulting architecture will exhibit the quality
properties considered by the perspective. For example, the Performance
perspective defines standard concerns such as response time, through-
put, and predictability. Understanding and prioritizing the concerns that
a perspective addresses helps you bring a firm set of priorities to later de-
cision making.
The perspective provides common conventions, measurements, or even a
notation or language you can use to describe the system’s qualities. For
example, the Performance perspective defines standardized measures
such as response time, throughput, latency, and so forth, as well as how
they are specified and captured.
The perspective describes how you can validate the architecture to demon-
strate that it meets its requirements across each of the views. For ex ample,
58
 P ART I  A RCHITECTURE F UNDAMENTALS
the Performance perspective describes how to construct mathematical
models or simulations to predict expected performance under a given
load and techniques for prototyping and benchmarking.
The perspective may offer recognized solutions to common problems,
thus helping to share knowledge between architects. For example, the
Performance perspective describes how hardware devices may be multi-
plexed to improve throughput.
The perspective helps you work in a systematic way to ensure that its
concerns are addressed by the system. This helps you organize the work
and make sure that nothing is forgotten.
P ERSPECTIVE P ITFALLS
As with any technique, you should take some care when applying perspec-
tives as there are some potential pitfalls.
Each perspective addresses a single, closely related set of quality property
concerns. There will often be conflicts between the solutions suggested by
different perspectives (e.g., a highly evolvable system may be less efficient,
and thus less performant, than a less flexible one). An important part of
your role as a software architect is to balance such competing needs.
The stakeholder concerns and priorities are different for every system, so the
degree to which you should consider each perspective varies considerably.
Perspectives contain established, general advice for ensuring that a sys-
tem exhibits certain quality properties. However, every situation is dif-
ferent, and it is important that you think about the advice and its
relevance to your situation and then apply it appropriately.
C OMPARING P ERSPECTIVES TO V IEWPOINTS
Since the first edition of the book was published, we’ve been asked a number
of times why we introduced the idea of architectural perspectives and didn’t
just define a set of viewpoints that addressed system qualities. So we thought
it was worth explaining in a little more detail why we introduced a new con-
cept rather than reusing an existing one.
The ISO standard for architecture definition, ISO 42010 (formerly known as
IEEE 1471), formalizes many of the concepts that we discuss in this book but
does not include the concept of a perspective. It addresses the cross-cutting na-
ture of perspectives by means of models shared across architecture views.
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 59
Sharing architecture models between architecture views permits an archi-
tecture description to capture distinct but related concerns without redun-
dancy or repetition of the same information in multiple views and reduces
possibilities for inconsistency.
Sharing of architecture models also permits an aspect-oriented style of
architecture description: Architecture models shared across architecture
views can be used to express architectural perspectives.
This approach is certainly workable and is broadly compatible with ours,
but we’ve found it valuable to treat perspectives as a distinct and separate
concept.
Both viewpoints and perspectives define concerns and the stakeholders
who have an interest in them, but the other information in viewpoints and
views, and describing the way it is used, is quite different. Viewpoints are
focused more on guiding the production of models that describe the architec-
ture, whereas perspectives are focused more on providing activities and tac-
tics to ensure that the system exhibits its required quality properties. Most
important, perspectives can be applied to one or more of the views, which
makes them fundamentally different for us.
We can compare and contrast our notions of view, viewpoint, and
perspective—probably the three most important concepts in this book—
as follows.
A view is a representation of all (or part of) an architecture—that is, a
way to document its architecturally significant features according to a
related set of concerns. A view captures a description of one or more of
the architectural structures of the system. Architects use views to explain
the architectural structure of the system to stakeholders and to demon-
strate that the architecture will meet their concerns. A view comprises a
set of tangible architectural products, such as principles and models; the
complete set of views of an architecture forms the AD.
A viewpoint guides the process of creating a particular type of view. A
viewpoint defines the concerns addressed by the view and the approach
for creating and describing that aspect of the architecture.
A perspective guides the process of design so that the system will exhibit
one or more important qualities. As such, a perspective can be considered
analogous to a viewpoint, but for a related set of quality properties rather
than a type of architectural structure. However, using a perspective usu-
ally results in changes to the architectural views (i.e., the system’s struc-
tures) rather than the creation of new structures. We also use
perspectives as a means of capturing common problems and pitfalls and
identifying solutions to them.
60
 P ART I  A RCHITECTURE F UNDAMENTALS
So in summary, while it’s certainly possible to create viewpoints that address qual-
ity property concerns, we’ve found a number of advantages to handling quality
properties a little differently using a distinct concept, that of the architectural per-
spective.
O UR P ERSPECTIVE C ATALOG
Part IV of this book defines several perspectives (see Table 4–2) that form a
set intended for application to the architectures of large-scale information
systems.
As we have said, there are many perspectives, and it is not usually fea-
sible or even desirable to consider all perspectives in the context of all of the
views. Not every perspective is relevant to every system and view, and in
fact it is rare that you will need to consider anywhere near the complete set
of perspectives for anything other than the largest and most complex
projects.
TABLE 4–2 P ERSPECTIVE C ATALOG
Perspective
Accessibility
Availability
and Resilience
Development
Resource
Evolution
Internationalization
Location
Performance and
Scalability
Regulation
Security
Usability
Desired Quality
The ability of the system to be used by people with disabilities
The ability of the system to be fully or partly operational as and when re-
quired and to effectively handle failures that could affect system availability
The ability of the system to be designed, built, deployed, and operated
within known constraints related to people, budget, time, and materials
The ability of the system to be flexible in the face of the inevitable change
that all systems experience after deployment, balanced against the costs of
providing such flexibility
The ability of the system to be independent from any particular language,
country, or cultural group
The ability of the system to overcome problems brought about by the abso-
lute location of its elements and the distances between them
The ability of the system to predictably execute within its mandated performance
profile and to handle increased processing volumes in the future if required
The ability of the system to conform to local and international laws, quasi-
legal regulations, company policies, and other rules and standards
The ability of the system to reliably control, monitor, and audit who can
perform what actions on which resources and the ability to detect and
recover from security breaches
The ease with which people who interact with the system can work effectively
C HAPTER 4  A RCHITECTURAL P ERSPECTIVES
 61
TABLE 4–3 M OST I MPORTANT PERSPECTIVES FOR T YPICAL S YSTEM T YPES
OLTP
 Calculation
 High-
Information
 Service/
 DSS/MIS
 Volume
 Enterprise
System
 Middleware
 System
 Web Site
 Package
Accessibility
 Varies
 Low
 Varies
 High
 High
Availability and
 Varies
 High
 Medium
 High
 Medium
Resilience
Development
 Medium
 High
 Medium
 High
 Low
Resource
Evolution
 Varies
 Low
 High
 Varies
 Medium
Internationalization
 Varies
 Low
 Varies
 High
 Varies
Location
 Varies
 Low
 Low
 High
 Varies
Performance and
 Varies
 High
 Varies
 High
 Varies
Scalability
Regulation
 Varies
 Low
 Varies
 Varies
 Varies
Security
 Varies
 Low
 Medium
 High
 High
Usability
 Medium
 Low
 Low
 High
 Medium
As with views, it is hard to provide generally applicable advice for which
perspectives to concentrate on, but to act as a starting point when planning
your work, Table 4–3 contains a set of suggested priorities for some typical
types of information systems.
S TRATEGY Apply only the most relevant perspectives to your views. Base
your selection on the needs of the stakeholders, the relative importance of the
different quality properties to them, and your own experience and judgment.
SUMMARY
Viewpoints and views are an excellent way to partition your architecture into a
set of interrelated models. However, these are often assessed for completeness
and correctness against only functional requirements, rather than against other
system qualities such as performance and scalability. This can result in a system
that is functionally correct but exhibits poor response time or is insecure or unre-
liable. A mechanism is required to make sure this doesn’t happen. It doesn’t
really make sense to use viewpoints to do this, because these system qualities
often have implications for many if not all of the other viewpoints. A separate but
related concept is required, which we call perspectives. We define a perspective as
62
 P ART I  A RCHITECTURE F UNDAMENTALS
a collection of activities, tactics, and guidelines you use to ensure that the system
will exhibit a particular set of related qualities, properties, or behaviors. Using
perspectives gives you a framework for the analysis and improvement of your
architectural models against the qualities the perspective addresses.
Applying a perspective to a view allows you to ensure that the architecture,
as represented in that view, is fit for its purpose as far as that perspective is con-
cerned. This is an iterative process: You create models in your views, assess
these models against criteria defined in the perspective, revise your view models
according to the outcome of this analysis, and iterate again.
We can compare and contrast our notions of view, viewpoint, and perspec-
tive as follows. A view is a representation of all (or part of) an architecture—
that is, a way to document its architecturally significant features according to a
related set of concerns; a viewpoint guides the process of creating a particular
type of view; and a perspective guides the process of designing the architecture
so that it exhibits one or more important qualities.
There are many perspectives, and it is not usually feasible or useful to apply
all perspectives to all views.
F URTHER R EADING
Standard books such as Software Architecture in Practice [BASS03], Evaluat-
ing Software Architectures [CLEM02], and Design and Use of Software Archi-
tectures [BOSC00] all discuss quality properties and are well worth reading for
more background in this area.
One particularly relevant area of software architecture research is the
study of tactics being undertaken at the SEI as part of its software architec-
ture program. The original definition of an architectural tactic can be found in
an SEI technical report from 2003 [BACH03]; sets of generic tactics for vari-
ous quality properties are outlined in Chapter 5 of Bass et al. [BASS03]; and
the SEI Web site (www.sei.cmu.edu) contains links to a number of technical
reports that discuss tactics further.
5
T HE R OLE OF THE
S OFTWARE A RCHITECT
I
 f describe you gathered the jobs a group they do, of you software would architects probably in end a room up with and at asked least a them dozen
 to
different definitions. More tellingly, if you asked the people with whom the
architects work how their colleagues fill their working hours, you would prob-
ably get still more definitions.
Our own practical experience supports this. On some projects, the person
with the title of architect has a very hands-on, directional involvement in the
nuts and bolts of designing, coding, and testing. Alternatively, architecture
may be viewed as an ivory tower from which pronouncements are handed
down at intervals to the build and implementation teams. Architect is also
often used as a generic title to denote a senior technical member of staff (such
as the Java architect that a number of organizations have).
Architects may specialize in one area, such as networking, middleware, or
database design, to the exclusion of others; occasionally, the architect may
not even have a system development background at all, having entered
through another route such as business analysis. The title may also be further
qualified in various ways such as application architect, data architect, or even
enterprise architect, without being clear what these roles involve.
So before we consider how you perform your job as an architect, we need to
understand exactly what that job is—what your responsibilities are, where your
boundaries are, what areas you should delegate to others, and how you work
alongside the other members of the team to ensure a successful sof tware delivery.
In this chapter, we establish a definition of the software architect’s role,
including what you are and are not expected to do to fulfill this role and what
qualities you need to possess in order to be a successful architect. We also
explore how this role relates to others involved in the product or system
development process.
63
64
 P ART I  A RCHITECTURE F UNDAMENTALS
T HEA RCHITECTURE D EFINITION P ROCESS
The last concept in our model of software architecture captures the process
used to design an architecture and create an AD for it. We call such a process
architecture definition.
DEFINITION Architecture definition is a process by which stakeholder
needs and concerns are captured, an architecture to meet these needs is
designed, and the architecture is clearly and unambiguously described via an
architectural description.
This process is often called architectural design, and we say this infor-
mally ourselves. However, in the book we tend to avoid this term because
of the potential confusion between its usage as a process and as an
artifact.
The goal of an architecture definition process is to design an architec-
ture that meets the needs of its stakeholders. There are a number of
aspects to this:
Capturing stakeholder needs, that is, understanding what is important to
stakeholders (possibly helping them reconcile conflicts such as function-
ality versus cost), and recording and agreeing on these needs
Making a series of architectural design decisions that result in a candi-
date architecture
Assessing the candidate architecture to determine how well it meets the
stakeholder needs
Refining the architecture until it is adequate
Capturing the architectural design decisions made and the resulting
architectural structures of the system in some form of AD appropriate to
the environment in which you are working
These activities form the core of the architecture definition process and are
normally performed iteratively. We talk more about this in Part II, in particular
how stakeholder needs and concerns relate to functional and architectural
requirements. For now, we’ll leave you with another principle.
P RINCIPLE A good architecture definition process is one that leads to a
good architecture, documented by an effective architectural description,
which can be realized in a way that is time- efficient and cost-effective for
the organization.
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 65
Architecture Definition Isn’t Just Design
A common question that arises is whether architecture definition is “just” part
of design or whether there is something more to it. It’s true that architecture
definition incorporates elements of design and also of requirements analysis,
but as we shall see in this book, it is a separate activity from each of these.
Design is an activity focused on the solution space and targeted primarily
at one group of people—the developers. It works within a clearly defined
set of constraints (the system’s requirements) and is essentially a pro-
cess of translating these into the specifications for a conformant system.
Historically, design has tended not to focus as much on the needs of
other groups such as operations or support, assuming that their needs
have been captured in the requirements specifications (or often ignoring
them altogether).
Requirements analysis, on the other hand, is an activity focused on the
problem space that (in its purest forms) ignores the needs and con-
straints of groups like developers and systems administrators because it
defines what is desired rather than what is possible. It also works within
a clearly defined set of constraints (the system’s required scope),
although within these constraints it tends to have much more freedom
than the design process does.
Architecture definition resolves this tension by bridging the gap between
the problem and solution spaces, as shown in Figure 5–1. Its focus is to
understand the needs of everyone who has an interest in the architecture, to
balance these needs, and to identify an acceptable set of tradeoffs between
these where necessary. The tradeoffs take into account the constraints that
exist (e.g., technical feasibility, timescales, resources, deployment environ-
ment, costs, and so on).
Although your role as a software architect incorporates elements of design
and of requirements capture, there are some key differences between it and the
other two roles, the most significant of which revolve around its scope.
PROBLEM
SPACE
Requirements
Analysis
Architecture
Definition
Software
Design
SOLUTION
SPACE
FIGURE 5–1 A RCHITECTURE D EFINITION , REQUIREMENTS A NALYSIS, AND S OFTWARE D ESIGN
66
 P ART I  A RCHITECTURE F UNDAMENTALS
You have to take input from a much wider range of people than just the
user community (as we have seen in our discussion of stakeholders).
You have to consider a much wider range of concerns than just function-
ality (as we have seen in our discussion of views and perspectives).
You have to consider the big picture as well as the details.
Architecture definition is often more a process of discovery than of just
capture. At the early stages when—with luck—you start to be involved, the
stakeholders may have only hazy ideas of their expectations of the system.
Furthermore, there may be a number of conflicting ideas about how the sys-
tem should be built, and there are likely to be big gaps in technical knowledge
and developer experience in the proposed solution elements.
Although theory says that you should not start to think about the solution
until you understand the problem—and we like this approach, as a theory—in
practice, stakeholders start to think about technology solutions from day one.
You can’t avoid this; you just have to manage it.
The Boundary between Requirements Analysis
and Architecture Definition
Part of your role as an architect is to be involved in the process of analyzing,
understanding, and prioritizing the system’s requirements. This also allows
you to start assessing the difficulty involved in implementing each requirement.
Strictly speaking, your role does not include requirements gathering, and
ideally you will be presented with a complete, consistent, prioritized list of the
key goals and requirements for the system. However, such a list often doesn’t
exist, and even when it does, requirements analysts often struggle to trade off
requirements against each other; while part of this process involves under-
standing the relative business value of requirements, it must also take into
account the associated costs and risks.
Some of the requirements specified initially are likely to be difficult to
implement because the requirements analysts have little or no insight into the
implementation options. As an architect, you are ideally placed to provide this
insight so that the importance of each requirement can be considered in the
context of the likely cost of providing it.
S TRATEGY Work with the requirements analysts to understand the system’s
requirements and their relative importance. For each important requirement,
consider the likely difficulty of implementing it and feed this back to the
requirements analysts to help them understand what can and cannot be
achieved.
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 67
The Boundary between Architecture Definition and Design
One of the most important decisions you will have to make as an architect is
whether something is important enough for you to worry about or whether it
can safely and more appropriately be left until the detailed design phase—in
other words, whether it is architecturally significant. Philippe Kruchten neatly
captured the essence of architectural significance in his definition, which we
paraphrase here.
D EFINITION A concern, problem, or system element is architecturally
significant if it has a wide impact on the structure of the system or on its
important quality properties such as performance, scalability, security, reliability,
or evolvability.
Predicting whether or not something will prove to be architecturally sig-
nificant is difficult and requires you to use your judgment, skill, and expertise
(and that of your stakeholders) to consider the circumstances of your particu-
lar project. For example, when a new technology is involved, questions
around reliability and performance may be very significant, whereas they may
be far less so in a system where the technologies are established and well
understood by the developers.
Your job as an architect is to ensure that you focus your and your stake-
holders’ attention on the important questions and decisions, which are those
that are likely to have a significant effect on the system’s ability to meet its
goals—this is something that will become easier with practice. Beware, how-
ever, of assuming that all architectural concerns are found at the abstract
level; often, the devil is in the details. You need to consider aspects of your
architecture at all levels, from the strategy to the code. It is also important to
keep considering whether your judgment is correct and to make sure that as
your architecture develops, you continue to review whether your scope is
appropriate.
EXAMPLE It isn’t always obvious whether something is architecturally
significant because you often don’t know what will end up having a big
impact on the system’s qualities. For example, consider the database
design: Beyond defining that the system will have a third-normal-form
data model and providing some guidance as to when denormalization
should occur, is the design of the database schema architecturally sig-
nificant? You certainly won’t be able to design the entire database
schema yourself except on the smallest projects.
68
 P ART I  A RCHITECTURE F UNDAMENTALS
As with many architectural decisions, this depends on the context. In
systems with relatively simple, straightforward data access patterns, we
would generally say that the detail of the database schema isn’ t architec-
turally significant, because it won’t have a big impact on the system’s
abilities to meet its quality goals. However, consider the situation where a
system makes extensive and complicated use of the database with many
very large queries, many of which are performance-critical. In this case,
we suggest that a lot of the database design detail is architecturally signif-
icant because many of those detailed decisions could have serious ramifi-
cations for the system’s performance and stability if made wrongly.
When considering the architectural significance of a decision, try to
look ahead and consider whether the different likely options for that
decision are going to impact the system’s key qualities. If some of the
options are likely to cause trouble in the future, you’ve found an archi-
tecturally significant decision. You can see why we say that this can be
difficult to do!
S TRATEGY As you are designing the architecture, review the areas you have
determined as being architecturally significant or not, and revise these as nec-
essary in the light of your deeper understanding of your stakeholders’ con-
cerns and of the architecture itself.
T HE R OLE OF THE A RCHITECT
We can now define the role of the architect in the following principle.
P RINCIPLE The architect is responsible for designing, documenting, and lead-
ing the construction of a system that meets the needs of all its stakeholders.
We see four aspects to this role:
1. To identify and engage the stakeholders
2. To understand and capture the stakeholders’ concerns
3. To create and take ownership of the definition of an architecture that
addresses these concerns
4. To take a leading role in the realization of the architecture into a physical
product or system
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 69
A common theme in most descriptions of what the architect does is some-
thing along the lines of “the architect owns the big picture.” We certainly sup-
port this view. One of your responsibilities as an architect is to develop and
maintain a high-level view of the main elements in the product or system,
which is subsequently used to guide detailed design, coding, testing, and
deployment.
But this isn’t all. You need to ensure that the architecture you d evelop is
right for your situation. As we have seen, every problem has a number of
possible architectural solutions, and every architecture has a number of pos-
sible representations. You must select an architecture that is fit for purpose
and then document that architecture in an appropriate way.
Traditionally, the architect is viewed as making primarily an up-front con-
tribution to system development—in other words, being heavily involved in the
inception stages of the project. However, your responsibility does not end there.
In fact, we find in general that the architect’s involvement during the software
development lifecycle conforms to the pattern illustrated in Figure 5–2.
This figure shows the architect’s depth of involvement during each major
development iteration of the system’s delivery. During the initial phases, your
involvement is intense. You are fully occupied in defining and agreeing on
scope, agreeing on and validating requirements, and providing the technical
leadership to make the decisions that will shape the architecture.
Your involvement typically lessens during the design, build, and test
phases, while the product or system is being built, tested, and integrated. In
practice, you may take a different role during this period, such as design
authority or designer. If so, you are likely to be involved in mentoring,
t
nemevlov s
n tI n e
f e t
 n
 cso m e o i nae T t th r n
 / at i r pp u g i e g ee q e s e d o e t c cD R D C n I ATime
FIGURE 5–2 THE A RCHITECT ’ S I NVOLVEMENT
70
 P ART I  A RCHITECTURE F UNDAMENTALS
reviews, problem resolution, and technical leadership. In any case, if the
architecture needs any changes, you must lead the change process.
Your involvement peaks again prior to and during acceptance, as you pro-
vide support and guidance to help resolve the last-minute problems that inevi-
tably occur and to ensure a smooth transition into the operational environment
and beyond.
S TRATEGY Stay involved with the development process beyond the creation
of the AD and through construction, acceptance, and handover (possibly at a
reduced level of involvement).
Architectural Leadership
In our experience, most organizations view “architect” as a technology lead-
ership role, although it may not always be clear in practice what this requires
you to do.
From a system standpoint, architectural leadership means the people-
focused activities that help ensure successful implementation of the system.
This includes the following:
Explaining and promoting the architecture to the business and technology
stakeholders, and justifying the principles and decisions that underpin it
Providing input to and support for planning and estimating tasks
Participating in change control processes
Taking responsibility for and signing off on the completion of technical
milestones
Helping to resolve issues that arise during development
Taking on more specific development roles such as design authority
Reviewing documentation and possibly code
Many architects also help to develop and promote the practice of architec-
ture within the organization in which they work. This may include arranging
or delivering architectural training; mentoring of more junior staff, perhaps
in a design role; developing viewpoints for the organization; or developing
and overseeing architectural governance processes such as architectural
reviews.
The extent to which you take on these responsibilities yourself, oversee
someone else, or delegate them completely will depend to a major extent on
the characteristics of your project and your own skills and aspirations. We
discuss this in more detail in Chapter 30, which closes the book.
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 71
Architectural
 relates
 Interelement
Element
 Relationship
2..n
 1..n
1..n
comprises
 comprises
1..n
guides
Architecture
the
 has an
Definition
 Architecture
 System
definition
Process
 1..n
of
follows
Architect
designs
creates
and
owns
can be documented by
0..n
Architectural
Description
captures the concerns of
comprises
documents architecture for
addresses the needs of
1..n
Stakeholder
1..n
1..n
 1..n
has
1..n
 1..n
conforms to
 addresses
View
 Viewpoint
 Concern
0..n
 1..n
 1..n
0..n
shaped by
1..n
0..n
Perspective
1..n
addresses
FIGURE 5–3 A RCHITECTURE DEFINITION AND THE A RCHITECT IN C ONTEXT
I NTERRELATIONSHIPS BETWEEN THE C ORE C ONCEPTS
We can now add two final pieces to our relationship diagram, namely, the
process of architecture definition and the architect, as shown in Figure 5–3.
We have added the following relationships to augment the earlier
versions of the model shown in previous chapters (e.g., Figure 4–3).
The architect captures and consolidates the concerns of the stakeholders.
The architect designs an architecture that meets these concerns.
72
A RCHITECTURALP ART I  A RCHITECTURE F UNDAMENTALS
The architect creates and owns the AD.
An architecture definition process guides the definition of the architecture.
The architect follows the architecture definition process to carry out all of
these tasks.
S PECIALIZATIONS
So far, we have viewed the architect as a generalist who deals with all aspects
of the system under development. This isn’t always the case, especially on
large projects where a team of architects may be working together. Everything
we talk about in this book—stakeholders, views, principles, models—applies
equally to such specialists, within their scope.
You are likely to see architects take on some of the following specializations.
Product architect: The product architect is responsible for the delivery of
one or more releases of a software product to external customers (and
typically would stay associated with the product over a number of release
cycles). The product architect is a key member of a product development
team and oversees the technical integrity of the product. One specific
challenge faced by the product architect is identifying user stakeholders,
especially before the first release.
Domain architect: Domain architecture is a specialization of the general
architectural function, focusing on a particular domain of interest, such
as the business architecture, data architecture, network architecture, and
so on. Domain architects are particularly valuable for working on large,
complex, or groundbreaking systems or for filling gaps in the knowledge
of the software architect.
Infrastructure architect: The infrastructure architect owns the provision
of hardware and software infrastructure to systems and often performs
this activity at a company-wide level. On the hardware side this may
include data centers, servers, storage and backup, desktop computers,
wide-area and local-area networking, office peripherals such as printers,
and specialist devices such as certificate servers. Infrastructure software
includes areas such as enterprise security, database management sys-
tems, enterprise messaging, identity and security, and desktop tools such
as word processing software. Information systems will often be
mandated to use all or some of these company-wide elements.
Solution architect: In contrast with the domain architect, the solution
architect specifically takes a broad, high-level view of the entire solution.
This role also focuses on wider issues than just technology, such as
business process change, procurement, staffing, and so forth.
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 73
Enterprise architect: Whereas the software architect concerns herself
with a single (albeit probably complex and important) system, the enter -
prise architect has responsibility for the cross-system information sys-
tems architecture of the whole enterprise, including sales and marketing,
client-facing systems, products and services, purchasing and accounts,
the supply chain, human resources, and so forth. The enterprise architect
is also often concerned with the definition and oversight of company-
wide principles, standards, and policies and, like the solution architect,
also concerns herself with wider issues such as business process change.
THE O RGANIZATIONAL C ONTEXT
Let’s look at how your role as a software architect compares with the roles of
the other key personnel on software development projects.
Business Analysts
A business analyst is responsible for capturing and documenting detailed
business requirements, typically focusing on stakeholders from the user com-
munity, and ensuring that these are correct, complete, and consistent. You
will often draw on the specialized knowledge of the business analyst, espe-
cially when dealing with views of interest to acquirers, users, and assessors.
Project Managers
A project manager is responsible for ensuring delivery of the product or system
and meeting commercial priorities for resources, costs, and timescales. You will
often help the project manager develop plans or assess them for reasonable-
ness. You will also provide the project manager with technical information,
feedback, advice, risk assessment, and so on throughout the project lifecycle.
In our experience the most productive relationship between project man-
agers and architects follows a partnership model: The project manager
focuses on stakeholders, plans, budgets, staffing and resources, milestones,
deadlines, and deliverables; and the architect focuses on stakeholders, con-
cerns, scope, requirements, views, and models.
Design Authorities
A design authority (sometimes referred to as a technical design authority or a
technical lead) takes overall responsibility for the quality of the internal
element designs for the system. In our experience, the architect often fills this
role as the project moves into the design phase. The design authority takes the
architectural views as her input and acts as guide and leader to the software
developers who design, build, test, and integrate the product or system.
74
 P ART I  A RCHITECTURE F UNDAMENTALS
We have found that design authority is often the role actually performed by
people who have the job title of technical architect. These key people are often
the primary technical points of contact for how the system is implemented and
how the underlying technology platform works. This role on the project is cru-
cial and must be filled by an extremely strong staff member. However, making
tradeoffs between requirements and possibilities for the system’s stakeholders
is not an inherent part of being a technical design authority, although it is a key
part of the architect’s role. Therefore we argue that the technical design author-
ity plays a design role rather than an architectural one.
The boundary between the design authority and the architect is probably
the hardest one to define formally. One guideline we find useful when deciding
whether an issue is architecturally significant is to consider its impact on stake-
holders. If the outcome of a decision is likely to have a significant impact on
important stakeholders or requires tradeoffs between stakeholder needs, the
architect should probably be responsible for the decision. If the decision is visi-
ble only within the development team, it is probably a design authority i ssue.
Of course, it is not always possible to make this assessment up front, and
it is essential that the two roles cooperate fully. Let’s consider a couple of
examples to see how this might work in practice.
EXAMPLE Architecture definition for a new system has identified the
need for a relational database, from the industry-leading supplier, for
persistent storage of transaction data. The imminent new release of the
database server will provide some significant new technology features
and potential improvements in performance.
Functionally, the system would look identical whether it were built on
the current or new version of the relational database management sys-
tem. However, taking on the new version presents some commercial risk
related to availability of skills, confidence in the new platform, and the
potential problems associated with any point-zero release.
Because of the possible commercial impact, we would tend to involve
the architect in this decision.
EXAMPLE In integration tests, some end-user queries have been falling
far short of their performance requirements, taking a minute or more to
complete under peak load. Monitoring and analysis have suggested that
some database tables need to be internally restructured, indexes
modified, and objects spread more evenly across physical disks. Access
to data, which occurs via stored procedures, will not be affected (other
than being much faster).
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 75
Because these changes have no visible stakeholder impact (other
than to make the system compliant), it seems reasonable not to involve
the architect in what are essentially internal systems decisions and to
instead make this the responsibility of the design authority.
Technology Specialists
A technology specialist provides detailed expertise in one specific area. Where
the architect provides breadth, the technology specialist provides depth, and
the combination of the two can be extremely powerful.
Broadly speaking, it is the technology specialist’s responsibility to provide
detailed facts, to assess the architecture for technical feasibility, and to spot
pitfalls early. You should be able to take the information provided by the tech-
nology specialist and apply it to addressing the problems you need to solve.
You should always make the best use of the skills and knowledge of your
colleagues in the organization. It’s not possible to know everything about
everything, and as an architect you aren’t expected to.
P RINCIPLE The architect provides and oversees the architectural breadth and
works closely with both business-focused and technology-focused specialists
who provide the specialist depth.
Developers
The architect’s involvement doesn’t end with handing over the completed and
accepted AD. Although your level of participation may decrease during the
build and test phases, you will still maintain a technology leadership role to
ensure that the team adheres to the spirit and the letter of the AD.
This may involve mentoring staff through the detailed design process,
reviewing designs as they are completed to ensure conformance to the sys-
tem’s architectural principles, arbitrating technology disputes, or even devel-
oping pieces of the implementation if required. You are likely to get involved
in integration and system testing to ensure that the tests exercise an appro-
priate selection of functional and operational characteristics.
You will also need to lead the change process if (as is likely) the AD
requires any modifications during development.
The nature of your interactions with your development team will depend
to some extent on the lifecycle model the team is following. An architect for a
large “waterfall” development program will interact very differently with her
developers than will an architect on a smaller iterative or agile development
project. We address this issue in Chapter 7.
76
T HEP ART I  A RCHITECTURE F UNDAMENTALS
A RCHITECT ’ S S KILLS
Although the job of the architect traditionally has a technology focus, and in
nearly all cases the architect herself has a strong technology background, we
have seen that the role is much broader than merely drawing up technical
plans and designs.
You must have an across-the-board understanding of technology at a
high level and of the real-world issues and problems the system is required to
solve. You should have real experience with designing and building systems,
although it may not always be possible to have direct, practical knowledge of
the specific technologies you plan to use. (This is an example of when you
must draw on the experience of technology specialists.)
Typically you will also have one or more areas of deeper technical exper-
tise; this may not apply to your current project but will give you the ability to
recognize a good design when you see one.
As well as technology knowledge, you also need to have a good understand-
ing of the business domain in which you are working. While you don’t need to
understand every detail of every process, you do need to understand the main
business processes and main types of information that are found in the business
area and the dependencies, importance, and criticality of each. This knowledge
will allow you to communicate more effectively with your business-oriented
stakeholders and will allow you to make more informed prioritization and
tradeoff decisions, as you will understand their likely i mpact on the business.
It is also very important that you have good “soft skills” as well, more so
than for many other IT roles, with the possible exception of the project man-
ager. These skills include:
Information capture: As we will see, you have to capture many types of
information, from a wide variety of different stakeholders, with different
interests in your architecture and different levels of business and techni-
cal expertise. You need to keep your stakeholders on track in interviews,
to get them to focus on the important architectural concerns, and to “drill
down” into detail where appropriate. You also need to be able to listen to
their answers and take notes at the same time!
Facilitation: Workshops and meetings can be a very effective way of cap-
turing information and mapping out potential solutions. However, man-
aging such a gathering can be quite a challenge, especially when you
have a mix of senior and more junior stakeholders, or when there is hid-
den (or explicit) conflict.
Negotiation: Reaching consensus among a wide variety of stakeholders
with often conflicting or incompatible concerns can also be a challenge.
Negotiation skills help you to understand and act on what is truly of
value to people and what they can afford to give away.
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 77
Communication: You may have the best architecture ever, but unless you
can communicate it effectively to all of your different stakeholders, and
get their buy-in, it is unlikely to be built. Different stakeholders have dif-
ferent interests and need to be communicated to in different ways—in
person or through documents, concisely or in great detail.
Flexibility: You need to be able to rapidly learn about unfamiliar business
areas and technologies, to make quick changes of direction where appro-
priate, and to be ready to discard your preconceived ideas about the prob-
lem or its solution. You also need to know when to hold your ground.
Above all, you must earn and maintain the confidence of all of your
stakeholders, from senior management and users to developers, third parties,
and operational staff.
THEA RCHITECT ’ S R ESPONSIBILITIES
A pro forma list of responsibilities for an architect would include the following
items.
Ensure that the scope, context, and constraints are documented and
accepted.
Identify, engage, and enfranchise your stakeholders.
Facilitate the making of system-level decisions, ensuring that they are
made on the basis of the best information and are aligned with stake-
holder needs.
Arbitrate and ensure that consensus is reached when stakeholder needs
are in conflict or are incompatible.
Arbitrate and ensure that consensus is reached when architectural com-
promises need to be made (for example, performance against flexibility
or security against ease of use).
Capture and interpret input from technical and domain specialists (and
represent this accurately to stakeholders as needed).
Define and document the architecture of the system.
Define and document strategies, standards, and guidelines to direct the
build and deployment of the system.
Ensure that the architecture meets the system quality attributes.
Develop and own the AD (i.e., manage all changes to it).
Help ensure that agreed-upon architectural principles and standards are
applied to the finished system or product.
Provide technical leadership.
78
 P ART I  A RCHITECTURE F UNDAMENTALS
S UMMARY
It is rare, in our experience, for such a role definition to exist in many
organizations. If you find yourself without one, you may find it helpful to cre-
ate one (use our list as a template) and get it agreed to and publicized with
your stakeholders. It should be a simple document that defines your architec-
tural scope (the tasks you will perform), your deliverables (the documents
and other material you will produce), and possibly the way you will work (for
example, that you will conduct architectural reviews with key stakeholders to
ensure that you are addressing their concerns).
S TRATEGY Ensure that you have clear terms of reference for your role on
any project in which you are significantly involved. If this does not already
exist, draw up a brief terms of reference document, and review and agree on it
with your stakeholders.
In many cases, you may also have some responsibilities for developing
and promoting the role of architecture in your organization, outside of your
involvement on specific projects. An obvious area of focus is the definition of
viewpoints; you may also find yourself involved in (or responsible for) the
development of architectural processes, tools, templates, and other materials.
We have discussed two distinct concepts in this chapter, the final chapter of
Part I.
Architecture definition is a process whereby stakeholder needs and con-
cerns are captured, an architecture to meet these needs is designed, and
the architecture is fully and unambiguously described via an AD.
The architect is the person (or group) responsible for designing, docu-
menting, and leading the construction of an architecture that meets the
needs of all its stakeholders.
There is no single commonly accepted definition of the software archi-
tect’s role. The role of the architect includes elements of requirements capture
and high-level design but is more than either of these. In this chapter, we
defined the four main responsibilities of the architect: to identify and engage
the stakeholders, to understand and capture their concerns, to create and take
ownership of the AD, and to take a leading role in the realization of the archi-
tecture.
We presented some architectural specializations that you may encounter
(or even choose to take on) such as product architects, domain architects,
infrastructure architects, solution architects, and enterprise architects. We also
C HAPTER 5  T HE R OLE OF THE S OFTWARE A RCHITECT
 79
compared and contrasted the role of the architect with other key project roles
such as business analysts, project managers, design authorities, technology
specialists, and developers. We considered when the architect is important: pri-
marily during the early stages of system development and during acceptance,
with a lesser role during the build and test phases.
Finally, we discussed the skills that a good architect should possess and
presented the architect’s responsibilities.
FURTHER R EADING
Most of the architecture books we mentioned earlier in Part I contain some
discussion of the architect’s role, for example, [CLEM10]. In addition, McGov-
ern et al. [MCGO04] contains a good discussion of roles related to software
and enterprise architecture.
Many books are available that can help you develop good soft skills, such
as information capture and communication skills, good examples being
[FISH03], [PELL09], and [BREN10]. These books will help you to identify
your weaknesses in these areas and give you a lot of practical ideas for im-
proving your skills, although in our experience the best way to learn soft
skills is through training, mentoring, and experience.
The definition of architecturally significant that we paraphrased earlier in
this chapter can be found in Kruchten [KRUC03].
This page intentionally left blank
P ART II
T HE P ROCESS O