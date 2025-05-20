21
T HE D EPLOYMENT
V IEWPOINT
Definition
Concerns
Models
Problems and
Pitfalls
Stakeholders
Applicability
Describes the environment into which the system will be deployed and
the dependencies that the system has on elements of it
Runtime platform required, specifi cation and quantity of hardware or
hosting required, third-party software requirements, technology
compatibility, network requirements, network capacity required, and
physical constraints
Runtime platform models, network models, technology dependency
models, and intermodel relationships.
Unclear or inaccurate dependencies, unproven technology, unsuitable
or missing service-level agreements, lack of specialist technical knowl-
edge, late consideration of the deployment environment, ignoring
intersite complexities, inappropriate headroom provision, and not
specifying a disaster recovery environment
System administrators, developers, testers, communicators, and assessors
Systems with complex or unfamiliar deployment environments
The Deployment view focuses on aspects of the system that are important
after the system has been built and needs to be validation tested and transi-
tioned to live operation. This view defines the physical environment in which
the system is intended to run, including the hardware or hosting environment
(e.g., processing nodes, network interconnections, and disk storage facilities),
the technical environment requirements for each type of processing node in
the system, and the mapping of your software elements to the runtime envi-
ronment that will execute them.
373
374
 P A R T III  A V I E W P O I N T C A T A L O G
A Deployment view is useful for any information system with a required
deployment environment that is not immediately obvious to all of the inter-
ested stakeholders. This includes the following situations:
Systems with complex runtime dependencies (e.g., specific third-party
software packages or particular network services are needed to support
the system)
Systems with complex runtime environments (e.g., elements are distrib-
uted over a large number of machines)
Systems hosted in third-party environments, such as hosting services or
public clouds, in order to allow a clear definition of the environment
required and how the system will deploy into it
Situations where the system may be deployed into a number of different
environments and the essential characteristics of the required environ-
ments need to be clearly illustrated (which is typically the case with
packaged software products)
Systems that need specialist or unfamiliar hardware or software in order to run
In our experience, most large information systems fall into one of these
groups, so you will almost always need to create a Deployment view.
C ONCERNS
Runtime Platform Required
The Deployment view must clearly identify the type of runtime platform that
the system needs and the role that each part of it plays. This includes general-
purpose compute nodes to host servers and computational logic, special-
purpose compute nodes to host database engines, storage for databases and
file systems, devices that allow users to access the system or print informa-
tion, network services required to meet certain quality properties (such as
firewalls for security), specialist hardware (such as cryptographic accelera-
tors), and so on. The manner in which the platform is provided, whether it be
physical hardware commissioned in-house, virtual servers and storage pro-
vided by a third-party hosting company, the use of a public cloud computing
environment, or some other option, needs to be clearly defined too, as does
the location of each part of the platform.
Defining the runtime platform involves identifying the general types of
processing elements required (such as compute server node, application
server node, storage array, and so on), defining the dependencies between
them, and mapping each of your functional elements to one of these types. In
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 375
effect, this is a logical model of the runtime platform that your system re-
quires. Then, when you have defined what each piece of the platform is used
for, you can think about the details of exactly what hardware elements you
need to provide it.
Specification and Quantity of Hardware
or Hosting Required
This concern, which follows from the previous one, addresses the specific
details of the hardware that will need to be procured and commissioned in order
to deploy the system—in effect, a physical model of the hardware your system
needs. This hardware may need to be ordered and commissioned in-house or
via a third party or may be specifications for a virtual computing environment,
such as ordering capacity from a cloud computing supplier.
This is a separate concern from the previous one because it is much more
specific and of interest to different stakeholders. For example, developers are
interested in whether the deployment platform will use Intel or Sun SPARC serv-
ers; whether the servers will run Linux, HP-UX, or Windows; and what general
processing resources will be available to them. However, system administrators
are interested in the detailed specification and quantity of the hardware ele-
ments or specification of the hosting environment that needs to be acquired to
create your runtime environment. The service-level agreements (SLAs) for each
part of the runtime environment will also need to be agreed to and validated as
acceptable for the level of service your system needs to provide.
Be specific when considering the specification, quantity, and service level
of the hardware and services that you need. If specific models of equipment or
specifications of hosted environment services are required, you need to
clearly identify and record them for easy reference. If specific models or ser-
vices aren’t required, you should still be precise where needed.
Third-Party Software Requirements
All information systems make use of third-party software as part of their
deployment environment—even if only an operating system. Many informa-
tion systems make use of dozens of third-party software products, including
operating systems, programming libraries, messaging systems, application
servers, databases, data movement products, Web servers, and so on. If you
are deploying your system to a platform-as-a-service environment, there is
probably a specific set of platform services and options that you need in order
for your system to run successfully.
Your Deployment view should make clear all of the dependencies between
your system and any third-party software products. This ensures that the
376
 P A R T III  A V I E W P O I N T C A T A L O G
developers know what software will be available for them to use and that the
system administrators know exactly what needs to be installed and managed
on each piece of hardware. It also helps you to spot any gaps in your analysis
as early as possible.
Technology Compatibility
Each software and hardware element in your system may impose require-
ments on other technology elements. For example, a database interface library
may require a particular operating system network library in order to function
correctly, or a disk array may require a particular type of interface in the
machines that will access it.
Furthermore, if you use a number of pieces of third-party technology
together, there is always the danger of uncovering incompatible requirements.
For example, your database interface library may require a certain version of
the operating system, but a graphics library you want to use isn’t supported
on that version. Such incompatibilities have a habit of emerging late in the
testing cycle and causing a lot of disruption—so if you consider them early,
you will avoid problems later.
Network Requirements
Your Functional and Concurrency views define the functional structure of
your architecture and make it clear how its elements interact. Part of the pro-
cess of creating the Deployment view is to decide which hardware elements
host each of these functional elements. Because elements that need to com-
municate often end up on different machines, some of the interelement inter-
actions can be identified as network interactions.
One of the concerns the Deployment view addresses is the set of services
that the system requires of its underlying network as a result of these network
interactions. This view needs to clearly identify the required links between
machines; the required capacity, latency, and reliability of the links; the com-
munications protocols used; and any special network functions the sy stem
requires (load balancing, firewalls, encryption, and so on).
Network Capacity Required
In our experience, software architects need to get less involved in specifying
network configuration than in identifying the processing and storage hard-
ware because the network is normally provided by a group of specialists who
design, implement, and operate the network for an entire organization.
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 377
However, this group needs to know how much network capacity your system
requires and the type of traffic you need to carry over the network. In order to
provide this information, you must estimate and record the amount and type
of network traffic that needs to be carried over each intermachine link in the
proposed network topology.
Physical Constraints
As software engineers we are lucky when compared to our colleagues work-
ing in other engineering disciplines. Normally, we don’t have to worry that
much about physical constraints because software has no weight, has no phys-
ical size, and occupies no physical space. However, when taking a sys tem-level
view, physical constraints suddenly become important again.
Considerations such as desk space for client workstations, floor space for
servers, power, temperature control, cabling distances, and so on may seem
relatively mundane. However, if someone doesn’t consider them, your system
simply won’t be deployed. There is no point in specifying four monitors for
each workstation if your users have desk space for only two. Similarly, if
there isn’t enough floor space in your data center for your servers, they won’t
be installed.
Stakeholder Concerns
Typical stakeholder concerns for the Deployment viewpoint include those
shown in Table 21–1.
TABLE 21–1 S TAKEHOLDERStakeholder Class
Assessors
Communicators
Developers
System administrators
Testers
C ONCERNS FOR THE D EPLOYMENT V IEWPOINT
Concerns
Types of hardware or hosting required, technology compatibility, and
network requirements
Types and specification of hardware or hosting required, third-party soft-
ware requirements, and network requirements (particularly topology)
Types and (general) specification of hardware or hosting required,
third-party software requirements, technology compatibility, and net-
work requirements (particularly topology)
Types, specification, and quantity of hardware or hosting required;
third-party software requirements; technology compatibility; network
requirements; network capacity required; and physical constraints
Types, specification, and quantity of hardware or hosting required;
third-party software requirements; and network requirements
378
 P A R T III  A V I E W P O I N T C A T A L O G
M ODELS
Runtime Platform Models
The runtime platform model is the core of this view. This description defines
the set of hardware nodes that are required, which nodes need to be con-
nected to which other nodes via network (or other) interfaces, and which
software elements are hosted on which hardware nodes.
A runtime platform model has the following main elements.
Processing nodes: Each computer in your system is represented by one
processing node in the runtime platform model. This allows you and
other stakeholders to see what processing resources are required for
the system. For situations where many similar machines are required
(e.g., Web server farms), you can use a summary notation (such as
UML’s shadow notation) to simplify the diagram, but make sure that the
number of nodes required is still clear.
Client nodes: You also need to represent client hardware, but probably in
less detail than the main processing hardware. You may have less control
over client hardware than server hardware, and if this is the case, you
need only represent the types and quantities of client machines required
rather than the precise details of each. If you have special needs for pre-
sentation or user interaction hardware (e.g., touch screens, printers),
this is specified as part of the client hardware.
Runtime containers: Client and server nodes may need to provide a
runtime container (such as a software application server or a client
virtual machine) to provide a suitable runtime environment for the
functional elements deployed onto them.
Online storage hardware: This defines how much storage is needed, of
what type, how it is partitioned, what it is used for, the assumptions you
are making about its reliability and speed, and whether or not processing
takes place close to its associated stored data. The storage hardware
could be disk devices within a processing node or dedicated storage
nodes such as disk arrays. Make the distinction between the two types
clear so that the physical impact of separate storage nodes on the deploy-
ment environment is understood. You need to include the capacity (and
possibly speed) of each type of storage hardware in the model.
Offline storage hardware: Despite the ever-growing capacity of online
storage hardware, many systems that deal with a lot of information still
require offline storage (archives) as well. Somehow the problems always
grow faster than the hardware capacity. Offline storage will also probably
be required to allow backup of information held online. You need to
ensure that there is sufficient capacity, that the hardware is fast enough
to complete archive and retrieval in an acceptable time, and that there is
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 379
sufficient network bandwidth between it and the online storage. The
requirements for the type, capacity, speed, and location of your offline
storage hardware all need to be defined here.
Network links: Your model needs to capture the essential connections
required by your system (rather than your ideas on how the network will
be built from specific network elements). It is sufficient at this point to
show the links between your hardware nodes; you’ll capture more details
about the network, such as internode bandwidth requirements, in the
network model (described next in this chapter).
Other hardware components: You may need to consider specialist hard-
ware for network security, user authentication, special interfacing to other
systems, or specialist processing (e.g., for automated teller m achines).
Runtime element-to-node mapping: The final element of this model is a
mapping of the system’s functional elements to the processing nodes
where they execute. How to go about defining this mapping depends on
how complex your concurrency structure is. If you have a Concurrency
view, you can map the operating system processes identified in that view
to the processing nodes. If you don’t have a Concurrency view, you can
map functional elements from the Functional view directly to processing
nodes (and in this case, presumably the details of the operating system
processes in use aren’t architecturally significant).
This runtime platform model is typically captured as a network node dia-
gram that shows nodes, storage, the interconnections required between the
nodes, and the allocation of the software elements between the nodes.
N OTATION Common notations used for capturing the runtime platform model
include the use of UML, traditional boxes-and-lines diagrams, and textual
notations. Each of these options is outlined in this subsection.
UML deployment diagram: You can use a UML deployment diagram to doc-
ument a runtime platform model. This diagram shows computing “nodes,”
and optionally “execution environments” (such as runtime containers),
with “artifacts” representing the software elements deployed to them and
the “communication paths” between the nodes (the communication path
being a specialization of a UML association). Interelement dependencies can
also be indicated on the diagram using regular or stereotyped UML depen-
dencies. Figure 21–1 shows an example of using a UML deployment dia-
gram as a simple runtime platform model that maps functional elements to
processing nodes, in some cases with execution environments.
When using the UML “artifact” to represent the software being deployed, it
may be useful to show the actual binary files that are deployed. Artifacts canFIGURE 21–1 E XAMPLE OF A R UNTIME P LATFORM M ODEL
also be used to represent entire system elements from the Functional view,
which can be clearer and simpler. We show both styles of artifact in the dia-
gram (e.g., the “OpsPlanner.jar” artifact is a deployed binary file, whereas the
“Data Capture Service” is a system element, which is probably composed of a
number of files). If you are using a UML tool to create your models, and the re-
lationships between system elements and deployed artifacts aren’t obvious,
you may wish to use the «deploy» dependency to record these relationships.
UML does not provide very specific semantics for the nodes and
communication paths and does not provide a library of predefined types
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 381
to choose from. Therefore, effective use of this diagram type usually
relies on the use of stereotypes, tagged values, and comments in order
to distinguish between different types of nodes and links. A runtime
platform model also needs to be augmented by plain-text descriptions
of the major elements, clearly defining the role and important charac-
teristics of each.
Boxes-and-lines diagram: Given the basic nature of the UML deployment
diagram, many architects choose a simple boxes-and-lines notation for
Deployment views. Boxes are used to represent nodes and elements, with
arrows for interconnection; the diagram is annotated as required in
order to make the meaning of each diagram element clear. With such an
approach, you need to carefully define the diagrammatic elements used
to avoid causing any confusion for the reader. This notation is easier to
draw with drawing tools that don’t support UML, and it may be more
comprehensible to nontechnical stakeholders.
Text and tables: Reference information such as required hardware speci-
fications is best represented by text that is organized into tables for easy,
unambiguous reference.
ACTIVITIES
Design the Deployment Environment. You typically start by identifying the key
servers in the system, any important client hardware requirements, and the net-
work links necessary between the nodes. With this done, you have the backbone
of your deployment environment. The rest of the process is normally elaboration,
adding any special-purpose hardware required (e.g., cryptographic accelerators,
or nodes for redundant capacity) and specifying the hardware and software con-
figurations for each node along with any interconnections.
Map the Elements to the Hardware. Once you have a proposed deployment
environment, you need to find a home in it for each of your functional (soft-
ware) elements. In reality, this is an iterative process where mapping the soft-
ware elements to hardware resources may suggest changes in the deployment
environment design (or newly identified deployment environment options
may suggest new alternatives for software element locations). The main chal-
lenges here relate to managing dependencies, ensuring that enough machine
capacity is available, and trading off the advantages of separated versus colo-
cated elements (e.g., security versus performance). Refer to Chapters 25 and
26 for more depth on these topics.
Estimate the Hardware Requirements. This activity normally starts with
some initial estimation before initial deployment environment design, followed
by an iterative process of refinement as architecture and design progress. The
resources you need to estimate include processing power, me mory, disk space,
and I/O bandwidth for each processing node.
382
 P A R T III  A V I E W P O I N T C A T A L O G
Conduct a Technical Evaluation. In order to design and estimate the deploy-
ment environment, you may need to perform a number of technical evaluation
exercises such as prototype element development, benchmarks, and compatibil-
ity tests. For example, you may wish to create a representative prototype sys-
tem to ensure that your application server, object persistence library, and
database all work smoothly together and to check the transaction throughput
you can achieve.
To ensure a representative test, identify the key attributes of your application
(size, type of processing, and so on) and make sure you include all of this in
your technical evaluation. Involve experts in the test to gain the benefit of their
experience and ensure that you do not overlook anything important.
Obtaining time and resources for technical evaluation is often a problem.
We have found that arguing for evaluation resources in terms of risk manage-
ment is often the most effective way to deal with this.
Assess the Constraints. It is rare for architects to be left to define a Deploy-
ment view without any external constraints. The constraints you encounter may
be formal standards, informal guidelines, or simply implicit constraints that you
know exist. However the constraints are expressed, you need to r eview your
proposed deployment environment design to ensure that they are met.
Network Models
In the interests of simplicity, the runtime platform description does not usu-
ally define the network in any detail. If the underlying network is complex, it
is usually described in a separate network model.
In our experience, the network is usually designed and implemented by
networking specialists rather than the software architect. However, it is impor-
tant that you provide the networking specialists with a clear specification of the
capabilities of the network you are expecting. This description must indicate
which nodes need to be connected, any specific network services that you re-
quire (such as firewalls or compression), and the bandwidth requirements and
quality properties required from each part of the network. This model is nor-
mally a logical or service-based view of what you require of the network, rather
than a physical view that specifies its individual elements. In the case of soft-
ware product development, such a model is a valuable specification for custom-
ers planning the deployment of your software.
The primary elements of a network model are as follows.
Processing nodes: The processing nodes represent your system elements
that use the network to transport data. This set of nodes should match
the set from the runtime platform model, but here they are abstracted to
simple elements with network interfaces.
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 383
Network nodes: Additional network nodes can be added to represent net-
work services that you expect to be available (such as firewall security,
load balancing, or encryption).
Network connections: The network connections are the links between the
network and processing nodes. They are elaborated to include the char-
acteristics of the service you expect the link to provide (most typically
bandwidth and latency, but perhaps also quality of service, reliability, or
other network qualities).
This description is typically represented as an annotated network dia-
gram, which is really a network-oriented specialization of the runtime envi-
ronment diagram. In cases where your network requirements are very simple,
you can describe the network sufficiently by elaborating the runtime platform
model, rather than creating a separate network model. However, given the
critical dependency that most of today’s systems have on the underlying net-
work, a separate network model is a useful tool to focus attention on this
aspect of the system.
Figure 21–2 shows a simple example of a network model for the runtime
platform we depicted earlier in Figure 21–1. This diagram would be aug-
mented with textual descriptions for each of the major elements.NOTATION Common notations used for capturing the network model include
the use of UML and traditional boxes-and-lines diagrams.
UML deployment diagram: UML’s deployment diagram is a useful base
notation for a network model. However, as with the runtime platform
description, you will probably need to annotate it with stereotypes,
tagged values, and comments in order to make your intentions clear.
Boxes-and-lines diagram: For reasons similar to those discussed earlier,
the network model is often drawn using an informal notation.
ACTIVITIES
Design the Network. The network design is typically handled separately from
that of the computer hardware because different specialists are involved. From
your point of view, this is a process of sketching what you need from the net-
work (in terms of connections, capacity, quality of service, and security). This re-
sults in what is effectively a logical rather than a physical network design, which
then becomes a specification for a specialist network designer to take further.
Estimate the Capacity and Latency. Part of designing your logical network
is to estimate the capacity and latency that you are expecting between each
node. Precision isn’t that important at this stage, but a realistic estimation of
the magnitude of the traffic to be carried and expected round-trip time is
important. You can estimate the capacity figures by combining peak transac-
tion throughput and a rough approximation of the size of messages required
to carry the transaction’s information. The latency is normally estimated
using a combination of standard metrics for the type of network in use (com-
bined with the distance between nodes) and some measurement of the exist-
ing network. Both results are normally combined with judicious scaling
factors to allow for inevitable overheads and prediction inaccuracies.
Technology Dependency Models
In some cases, you can manage the dependencies within your development or
test environment by bundling your software and its dependencies into one
deployment unit. However, in many cases this simply won’t be possible for
reasons such as efficiency, cost, licensing, or flexibility. If this is the case, you
need to manage the dependencies in your deployment environment.
Technology dependencies are usually captured on a node-by-node basis
in simple tabular form. The software dependencies are typically derived from
the Development view, where you define the environment used by the soft-
ware developers. You can also derive hardware dependencies from test or
development environments, but in many cases you have to rely on manufac-
turer specifications and some judicious testing to confirm them.
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 385
TABLE 21–2 S OFTWARE D EPENDENCIES FOR THE P RIMARY S ERVER NODE
Component
 Requires
Data Access Service
 HP-UX 64-bit 11.23 + patch bundle B.11.23.0703
HP aCC C++ runtime A.03.73
Data Capture Service
 HP-UX 64-bit 11.23 + patch bundle B.11.23.0703
HP aCC C++ runtime A.03.73
Oracle OCI libraries 11.1.0.7
HP aCC C++ Compiler & Runtime
 HP patch PHSS_35102
HP patch PHSS_35103
Oracle OCI 11.1.0.7
 HP-UX optional package X11MotifDevKit.MOTIF21
HP-UX patch PHSS_37958
EXAMPLE Table 21–2 shows an example of software dependencies for
the Primary Server node in our example from Figure 21–1.
From this table it is possible to see that this node in the system needs
a particular version of HP-UX with a patch bundle, a couple of specific
operating system patches, a set of C++ libraries, and one optional mod-
ule installed, as well as a particular version of an Oracle product.
In simple cases, it may be possible to use the Development view contents
rather than list dependencies in this view. However, in more complex cases, it
is unlikely that the Development view contains the detail required to fully
define the software dependencies for each node type in the system.
N OTATION A technology dependency model is often best captured by using
a simple text-based approach, but it can sometimes benefit from the use of
some simple graphical notations.
Graphical notations: One way to capture software dependencies is to
extend your runtime platform model to add an indication of the software
stack required on each machine to support the system elements execut-
ing there. In simple cases, this can be a useful elaboration of the runtime
platform model. The problem with this is that complete and accurate soft-
ware dependency stacks on each node can clutter the runtime platform
model to the point where it is no longer usable—in this case, you should
record this information separately.
386
 P A R T III  A V I E W P O I N T C A T A L O G
Text and tables: Dependencies are almost always captured as simple text
tables. It is important to capture the exact requirements for third-party
software (e.g., detailed version numbers, option names, and patch levels).
ACTIVITIES
Analyze the Runtime Dependency. This is usually a manual exercise to work
through your system elements, identifying the dependencies they have and
then repeating this process for each of the third-party elements. You normally
derive the runtime dependencies from documentation supplied with each piece
of third-party technology you are using and your own build and test environ-
ment requirements. With this done, you can clearly define the third-party ele-
ments you need for each processing node in the system.
Conduct a Technical Evaluation. In order to correctly document dependen-
cies, you may need to do some prototyping or technical investigation.
Intermodel Relationships
For complex systems, a Deployment view contains two or three closely related
models rather than a single model. We have found that the three models
described earlier tend to be used by different stakeholders at different times.
People in the groups responsible for deployment refer to the runtime platform
model early in the project, a specialist networking group consults the network
model, and system administrators use the technology dependency model dur-
ing more detailed installation planning close to deployment. For this reason,
we’ve found it valuable to present each separately.
A good way to think about these models is as a set of informal layers,
with the core of the view being the runtime platform model. You can think of
the network model as a lower layer supporting the runtime platform by defin-
ing details of the network required. The technology dependency model can be
thought of as a more detailed layer on top of the runtime platform that defines
the software and hardware installation requirements on each machine in the
deployment environment.
In an ideal world, a software architecture tool would allow you to create a
single model for yourself and then extract different aspects of it automatically as
required. However, we aren’t aware of the existence of any such tool today, and
so you’ll probably have to work with separate models for the foreseeable future.
Figure 21–3 illustrates this relationship between the models within the
Deployment view. The runtime platform model is the core of the view, with
the network model providing more details of the network underpinning the
system and the technology dependency model providing more detail about
the hardware and software installed on each node to provide the runtime
environment.P ROBLEMSFIGURE 21–3 M ODELS IN D EPLOYMENT V IEW
AND P ITFALLS
Unclear or Inaccurate Dependencies
Large-scale computing technology tends to be fairly complex, and it often has
many explicit and implicit dependencies on its runtime environment that will
cause problems if not satisfied. This difficulty is compounded by the fact that
most of these dependencies are invisible and can’t be checked easily—you
may not discover that you have the wrong version of a utility library until
your database server fails to start.
“You need Oracle and Linux” or “It uses Intel hardware” are pretty com-
mon dependency statements. For all but the smallest systems, these are too
vague to allow safe deployment of the system. You should specify which ver-
sions are required, whether any optional parts of the products are needed,
whether any patches are required, and so on. With the complexity and flexi-
bility of enterprise software products today, you need to be very clear about
what is required and what isn’t.
RISK REDUCTION
 Capture clear, accurate, detailed dependencies between your software
elements and the runtime environment in the Deployment view.
 Capture dependencies between third-party software and the runtime
environment it needs.
388
 P A R T III  A V I E W P O I N T C A T A L O G
Perform compatibility testing to ensure that the dependencies between
the elements are correct.
Use existing, proven combinations of technologies where the dependencies
are well understood.
Unproven Technology
Everyone wants to use the newest and coolest technology—and understand-
ably so, as it often has the potential to bring great benefits. However, because
its characteristics are unknown, using technology with which you don’t have
experience brings significant risks: functional shortcomings, for example, or
inadequate performance, availability, or security.
RISK REDUCTION
 As much as possible, use existing software and hardware that you can
test before committing to its use.
 When you must use new technology (or technology new to you), get
advice from people who have used the technology before, or if this is not
possible, test it thoroughly.
 Create realistic, practical prototypes and benchmarks to make sure that
technologies work as advertised.
 Perform compatibility testing to ensure that new technologies work well
with existing technologies.
Unsuitable or Missing Service-Level Agreements
The runtime environment for your system is usually provided by other people,
whether they are a separate part of your organization or are a completely sep-
arate organization. When providing services such as hardware, data storage,
networking, and so on, it is usual to specify an SLA to define the service that
you can expect from the provider. This will cover aspects of the service such
as cost, expected performance and reliability, recovery time guarantees in
case of failure, data backup service, and so on. You need to check the SLAs
carefully to make sure that the guarantees that they provide will allow you to
meet the goals of your system.
RISK REDUCTION
 Obtain a reliable SLA for the runtime environment elements that are
provided by third parties (and estimate your own SLA if providing
elements yourself).
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 389
Attempt to test the guarantees that the SLAs provide.
Analyze the SLAs to understand how they combine and the implications
of their combination.
Lack of Specialist Technical Knowledge
Designing a large information system is a complex undertaking that requires
a huge amount of specialist knowledge about many different subjects. No
one person can possibly be an expert on all of the technologies you may
need to use. This is why we use teams of people to develop systems and why
some people specialize in particular technologies, allowing them to advise
others.
Given the number of technologies used in many systems, it can be diffi-
cult to assemble a project team with expertise in all of the technologies
required. This can lead to a situation where you end up relying on vendor
claims for products rather than proven knowledge and experience.
RISK REDUCTION
 Bring specialist knowledge into your team so that you have mastery of
all of the key technologies you need to use to deliver your system. If you
don’t need the knowledge full-time, hire trusted and experienced part-
time experts.
 Obtain external expert review of your architecture to validate your
assumptions and decisions.
 Obtain binding contractual commitments from your technology suppliers
when possible.
Late Consideration of the Deployment Environment
The deployment environment is where your system hits reality. We’ve seen
problems in some projects when the system is designed from a purely soft-
ware-oriented perspective and the deployment environment is considered
only when the software is complete. Remember that an inappropriate
deployment environment can make an otherwise good system totally
unusable.
The deployment environment also often affects how the software is
designed and implemented, and this can be expensive to change. For exam-
ple, if plans change and you need to use a group of small machines rather
than a single large machine to host your server elements, this could have a
significant impact on the architecture of your server software, a change that
would be expensive to make late in a project.
390
 P A R T III  A V I E W P O I N T C A T A L O G
RISK REDUCTION
 Design your deployment environment as part of architecture definition
rather than as part of a separate exercise performed after the system has
been developed.
Obtain external expert review of your architecture to get early feedback
before you spend too much time or money.
Ignoring Intersite Complexities
Many systems are deployed to an environment involving more than one phys-
ical site, and this is becoming ever more prevalent as organizations move to
use third-party hosting providers and cloud computing environments to aug-
ment their own data centers. Even when the entire environment is hosted in-
house, concerns such as resiliency, disaster recovery, geographical location of
the business, and data movement restrictions can result in systems being
hosted across a number of geographically distant sites.
If you do have a multisite deployment environment, it is important to con-
sider the impact of this quite early in your architectural design work as it can
have a major impact on the quality properties of the system, particularly its secu-
rity, performance, and scalability. Network latency between sites is the most ob-
vious problem (meaning that interelement interactions across these links need to
be considered carefully), but the need to keep the system secure across multiple
sites and the possible scalability limitations of needing to synchronize informa-
tion across sites are some of the other areas of concern that need to be addressed.
RISK REDUCTION
 Understand any requirements for multisite deployment as early as pos-
sible in your design work, and if it looks likely that multisite deploy-
ment is going to be required, consider its impact on all of your system
qualities.
 Work with your infrastructure teams to understand the implications of
distributing your system to multiple sites and the restrictions that the
infrastructure may impose on this.
 Try to test various representative aspects of multisite deployment as soon
as you can so that you are confident that you understand its implications.
Inappropriate Headroom Provision
Headroom is additional capacity (CPU power, memory, disk space, network
bandwidth, and so on) that you include in your hardware specifications to
accommodate spikes in demand or future growth in volumes. You usually add
C HECKLIST
C H A P T E R 21  T H E D E P L O Y M E N T V I E W P O I N T
 391
some headroom to your sizing estimates so that your system can cope with
additional demand without incurring hardware upgrade costs.
Specifying headroom involves a delicate balance between optimism
about future growth and spending restraint. If you get it wrong, you end up
deploying either expensive hardware that is insufficiently used or a system
that fails to meet its performance requirements. We discuss this further in
Chapter 26.
RISK REDUCTION
 Make sure your hardware specifications include an appropriate amount
of headroom. Refer to the Performance and Scalability perspective, dis-
cussed in Chapter 26, for a discussion of how to model this effectively.
Not Specifying a Disaster Recovery Environment
Disaster recovery is the means whereby systems can be kept operational in
the event of a significant failure, such as loss of electric power, widespread
storage failure, or a natural disaster such as fire or flood.
Many disaster recovery strategies require the deployment of a separate
operational environment at a different location (for example, a standby or al-
ternate data center). To keep costs down, the standby environment may have a
lower specification than the production environment. In any case, as it is usu-
ally the responsibility of a development project to specify, implement, and pay
for the standby hardware, this must form part of your architectural description.
We discuss this further in Chapter 27.
RISK REDUCTION
 Make sure your Deployment view includes a specification of any disaster
recovery hardware required.
Have you mapped all of the system’s functional elements to a type of ele-
ment in your runtime platform? Have you mapped them to specific hard-
ware devices if appropriate?
Is the role of each piece of your runtime platform fully understood? Is the
specified hardware or service suitable for the role?
Have you established detailed specifications for the system’s hardware
devices or the hosted services that you require? Do you know exactly
how many of each device or how much of each service is required?
Do you have service-level agreements for the elements of the runtime
environment that are supplied by third parties? Are the guarantees in the
392
F URTHERP A R T III  A V I E W P O I N T C A T A L O G
agreements suitable for your system? Can you test whether the guaran-
tees are credible or not?
Have you identified all required third-party software and documented all
the dependencies between system elements and third-party software?
Are the network topology and services required by the system understood
and documented?
Have you estimated and validated the required network capacity? Can
the proposed network topology be built to support this capacity?
Have network specialists validated that the required network can be built?
Have you performed compatibility testing when evaluating your architec-
tural options to ensure that the elements of the proposed deployment
environment can be combined as desired?
Have you used enough prototypes, benchmarks, and other practical tests
when evaluating your architectural options to validate the critical aspects
of the proposed deployment environment?
Can you create a realistic test environment that is representative of the
proposed deployment environment?
Are you confident that the deployment environment will work as
designed? Have you obtained external review to validate this opinion?
Are the assessors satisfied that the deployment environment meets their
requirements in terms of standards, risks, and costs?
Have you checked that the physical constraints (such as floor space,
power, cooling, and so on) implied by your required deployment environ-
ment can be met?
Do your hardware and service specifications include an appropriate
amount of headroom?
Does your Deployment view include a specification of a disaster recovery
environment, if required?
R EADING
A great deal of literature describes specific deployment technologies; unfortu-
nately, little of it discusses how to design an entire realistic and reliable system
deployment environment. Some other software architecture books [CLEM10,
GARL03, HOFM00] contain useful explanations of how to document deployment
views. Dyson and Longshaw’s book on designing large-scale applications
[DYSO04] includes a number of patterns relating to the Deployment view. Some
of the further reading we recommend in the perspectives in Part IV also contains
principles and patterns relevant to the design of a deployment environment.