23
A CHIEVING C ONSISTENCY
ACROSS V IEWS
T
 he architect: use of to views represent addresses a large one andof complex the biggest system challenges in a way your you face stakehold-
 as an
ers can understand. A view is a way to portray those aspects or elements of the
architecture that are relevant to the concerns the view intends to address—and,
by implication, the stakeholders for whom those views are important.
Without views, you end up with a single, all-encompassing model that
tries (and usually fails) to illustrate all of the aspects of your system. Such a
model is complex, uses a mix of notations, and is too hard for anyone to
understand—never mind appreciate the subtleties, nuances, and implications
of your architectural choices.
However, the problem with partitioning the representation of your archi-
tecture through using views is that it is difficult to ensure consistency
between them—in other words, to ensure that the structures, features, and
elements that appear in one view are compatible and in alignment with the
content of your other views. This consistency is a vital characteristic of your
AD—without it, the system will not work properly, will not achieve its design
goals, and may even be impossible to build.
Unfortunately, although some design tools can simplify the process of
creating your models, we are not aware of any currently available tool that
will automate such consistency checking to the extent that you need it to. The
use of formal modeling languages such as UML only partially addresses this
problem, and the tools that support these languages typically provide only
basic features for checking one model for consistency against another. And,
of course, if you are using an informal notation or one you have developed for
your specific situation, the problem is even worse.
Ensuring consistency between views therefore largely comes down to
the skill, thoroughness, and diligence of the architect and (to a lesser extent)
425
426
 P A R T III  A V I E W P O I N T C A T A L O G
the stakeholders. We have found the following strategies to be helpful in
achieving inter-view consistency.
Focus on consistency from the outset: We saw that trying to apply quality
properties after the fact doesn’t work—good performance, availability,
and resilience have to be designed into your solution from the start. Sim-
ilarly, it is no good waiting until your models are nearly complete to
determine whether they are consistent with one another: More likely
than not, they won’t be, and you will be faced with a significant piece of
rework and additional review.
Enumerate model elements: Assigning each significant model element a
unique identifier simplifies the process of asking such questions as “Is
element 3 from Model B consistent with element 5 from Model D?”
Ensure that consistency checks are a formal part of reviews : Consis-
tency should be one of the criteria you use to review models and other
architectural documentation. This means both internal consistency
(Is this part of the model consistent with other parts of this model?)
and external consistency (Is this model consistent with other models
that make up the AD?). If you perform such a formal consistency check,
you should include its results (and the actions taken) in an appendix to
your AD.
R ELATIONSHIPSBETWEEN V IEWS
Although all of the views are obviously interrelated, in practice there are
strong dependencies only between some of the views. The UML class diagram
in Figure 23–1 shows the most important of these dependencies. The relation-
ships illustrate a strong dependency, which implies that if something changes
at the end of the arrow, a change will probably be required at the start of the
arrow.
Conversely, if there’s no dependency between two views, changing some-
thing in one is unlikely to itself necessitate a change in the other. (So chang-
ing a Development view element, for example, does not in itself imply any
changes to the Functional models—unless you are changing it for a reason
not to do with development, of course.)
Note that if you don’t develop a particular view—for example, if you
encapsulate the concurrency aspects of the architecture in the Functional
view, rather than in a separate Concurrency view—it is still useful to apply the
checklists presented in this chapter for that view, to ensure that you have
addressed its most important concerns.
C ONTEXTAND F UNCTIONAL V IEW C ONSISTENCY
Goal: To ensure that the system scope and requirements are fully and c orrectly
implemented by the system.
Does each requirement map to one or more functional elements that
implement that requirement?
Is every functional element necessary (directly or indirectly) in order to
implement at least one requirement?
Has every quality property that affects system functionality been taken
into consideration in the system structure defined by the Functional view?
Is every external entity defined in one view also present in the other
view, and do they have the same definition in each view?
Is every interface defined in one view also present in the other view, and
do they have the same definition (responsibilities, nature, and character-
istics) in each view?
Are the interaction scenarios defined in the Context view compatible with
the functional structure of the system and the way its elements interact
with one another and the outside world?
C ONTEXT AND INFORMATION VIEW CONSISTENCY
Goal: To ensure that data flows in and out of the system are compatible with
the information management approach defined in the Information view.
428
 P A R T III  A V I E W P O I N T C A T A L O G
Has consideration been given in the Information view to all of the data
items identified in the Context view that flow into the system (owner-
ship, consistency, timeliness, and so on)?
Has consideration been given in the Information view to all of the data
items identified in the Context view that flow out of the system (owner-
ship, consistency, timeliness, and so on)?
Has every quality property that affects information management been
taken into consideration in the Information view?
Is the data ownership model in the Information view (particularly when
data is owned by external entities) compatible with the responsibilities
defined for external entities in the Context view?
Is the high-level data model in the Information view compatible with the
data models used by external systems, or if not, have appropriate mecha-
nisms for data transformation been defined?
If external archiving services are defined in the Information view, are
they represented as external entities in the Context view?
C ONTEXT AND D EPLOYMENT V IEW C ONSISTENCY
Goal: To ensure that external connections between this system and others can
be supported in the planned deployment environment.
Do all external entities that represent systems, interfaces, or other
technology-based connections appear consistently in both the Context
and the Deployment views?
Does the Deployment view contain all of the hardware and software required
to communicate with the external entities identified in the Context view?
Is the technology used for each interface in the Deployment view appro-
priate for its nature and characteristics as defined in the Context view?
Are system elements that communicate with external entities deployed to
parts of the deployment environment where external communication is
possible (e.g., to a DMZ in the network)?
Has every quality objective identified in the Context view that affects the
deployment environment been taken into account in the Deployment view?
F UNCTIONAL AND INFORMATION VIEW CONSISTENCY
Goal: To ensure that the functional and information structures are compatible
and that nothing is missing in one that is required by the other.
C H A P T E R 23  A C H I E V I N G C O N S I S T E N C Y A C R O S S V I E W S
 429
Does every nontrivial functional element in the Functional view that
needs persistent data have corresponding data elements in the Informa-
tion view?
Does every nontrivial data element in the Information view have at least
one element in the Functional view that is responsible for the mainte-
nance of that data?
If information flows are described in the Information view, are they con-
sistent with the interelement interactions in the Functional view?
If the Information view requires specific functional features (e.g., distrib-
uted transaction support, redundant logging of updates, and so on), are
these features addressed in the Functional view?
Do the data ownership models in the Information view align with the
functional structure in the Functional view?
If the data ownership characteristics are complex (e.g., multiple creators
or updaters), do the functional models reflect the requirements for main-
taining distributed data consistency?
If there are significant issues around the maintenance of distributed
identifiers (keys), do the functional models include features to address
these problems?
If the architecture has significant data migration and data quality analy-
sis aspects, are there functional elements for these in the Functional
view?
If the functional structure has loose coupling as an architectural goal, is
this reflected (as far as possible) in the static information structure?
FUNCTIONAL AND C ONCURRENCY V IEW C ONSISTENCY
Goal: To ensure that the functional elements are all mapped to tasks that will
allow them to execute and that the interelement interactions are supported by
interprocess communication mechanisms if required.
Is every functional element in the Functional view mapped to a concur-
rency element (a process or thread) responsible for its execution in the
Concurrency view?
If functional elements are partitioned into separate processes, are suit-
able interprocess communication mechanisms used to allow all of the
interelement interactions shown in the Functional view?
If multiple functional elements are packaged into a single process, is it
clear which element controls the process?
430
 P A R T III  A V I E W P O I N T C A T A L O G
F UNCTIONALAND DEVELOPMENT V IEW C ONSISTENCY
Goal: To ensure that all of the functional elements are mapped to a design-
time module and to ensure that the common processing, test approach, and
codeline specified are all compatible with and can support the proposed func-
tional structure.
Does the code module structure include all of the functional elements that
need to be developed?
Does the Development view specify a development environment for each
of the technologies used by the Functional view?
If the Functional view specifies the use of a particular architectural style,
does the Development view include sufficient guidelines and constraints
to ensure correct implementation of the style?
Where common processing is specified, can it be implemented in a
straightforward manner over all of the elements defined in the Func-
tional view?
Where reusable functional elements can be identified from the Func-
tional view, are these modeled as libraries or similar features in the De-
velopment view?
If a test environment has been specified, does it meet the functional
needs and priorities of the elements defined in the Functional view?
Can the functional structure described in the Functional view be built,
tested, and released reliably using the codeline described in the Develop-
ment view?
F UNCTIONAL AND DEPLOYMENT V IEW C ONSISTENCY
Goal: To ensure that each of the functional elements is correctly mapped to its
deployment environment.
Has each functional element been mapped to a processing node to allow
it to be executed?
Where functional elements are hosted on different nodes, do the network
models allow the required element interactions to occur?
Are functional elements hosted as close as possible to the information
they need to process?
Are functional elements that need to interact extensively hosted as close
together as possible?
C H A P T E R 23  A C H I E V I N G C O N S I S T E N C Y A C R O S S V I E W S
 431
Are the specified network connections sufficient for the needs of the
interelement interactions that will be carried over them (in terms of
capacity, reliability, security, and so on)?
Is the hardware specified in the Deployment view the most efficient
solution for hosting the specified functional elements?
FUNCTIONAL AND O PERATIONAL V IEW C ONSISTENCY
Goal: To ensure that each of the specified functional elements can be installed,
used, operated, managed, and supported.
Does the Operational view make it clear how every functional element
will be installed (and upgraded if necessary)?
If migration is required, does the Operational view make it clear how
migration will occur to every functional element that needs it?
Does the Operational view explain how each functional element will be
monitored and controlled in the production environment?
Does the Operational view explain how the configuration of each func-
tional element will be managed in the production environment?
Does the Operational view explain how the performance of each func-
tional element will be monitored in the production environment?
Does the Operational view explain how each functional element will be
supported in the production environment?
Are the approaches that the Operational view specifies for installation,
migration, monitoring, control, and support the simplest set that will
support the needs of the system’s functional elements?
I NFORMATION AND C ONCURRENCY V IEW C ONSISTENCY
Goal: To ensure that the concurrency structure of the system does not cause
data access problems and that the proposed information structure is compati-
ble with the concurrency structure.
Does the concurrency design imply concurrent access to any of the sys-
tem’s data elements? If so, have the data elements been protected from
concurrent access problems?
When functional elements are packaged into operating system processes,
is the data they require still available to them?
If functional elements that share data elements are packaged into differ-
ent operating system processes, has a suitable interprocess data-sharing
mechanism been defined?
432
 P A R T III  A V I E W P O I N T C A T A L O G
I NFORMATION AND D EVELOPMENT V IEW C ONSISTENCY
Goal: To ensure that the proposed development environment can provide the
technical resources required to develop the data management aspects of the
system.
Does each data management technology identified in the Information
view have development tools and the environment defined for it?
Does the sizing of the development environments and test data platforms
reflect the data volumes created in the Information view?
If the Information view defines a significant migration data aspect, are
there development tools and environments defined to support this?
If the Information view defines external data components (e.g., for exist-
ing systems or external systems under construction), does the Develop-
ment view take this into account (e.g., the creation of stub environments,
realistic test data, and so on)?
I NFORMATION AND D EPLOYMENT V IEW C ONSISTENCY
Goal: To ensure that the proposed deployment environment provides the
resources required to support the defined information structure.
Does the Deployment view include enough storage (of the appropriate
types) to support the information storage approach specified by the
Information view?
If separate storage hardware is used, does the Deployment view specify
sufficiently fast and reliable links from the storage to the processing
hardware?
Does the Deployment view reflect the requirements for backup and re-
covery as addressed by the Information view?
If large volumes of information need to be moved, is sufficient band-
width available so that this can be achieved without critically impacting
the operation of the system?
I NFORMATION AND O PERATIONAL V IEW C ONSISTENCY
Goal: To ensure that the system’s information structure can be installed, used,
operated, managed, and supported.
C ONCURRENCYC ONCURRENCYC H A P T E R 23  A C H I E V I N G C O N S I S T E N C Y A C R O S S V I E W S
 433
Does the Operational view make it clear whether specific installation
steps are required for the system’s data management technology?
If migration is required, does the Operational view make it clear how data
migration will occur?
Does the Operational view explain how the data management technology
will be monitored and controlled in the production environment?
Does the Operational view explain how the configuration of the data man-
agement technology will be managed in the production environment?
Does the Operational view explain how the performance of the data man-
agement technology will be monitored in the production environment?
Does the Operational view explain how the data management technology
will be supported in the production environment?
AND D EVELOPMENT V IEW C ONSISTENCY
Goal: To ensure that the concurrency structure specified in the Concurrency
view can be built and tested in the development environment specified by the
Development view.
If the concurrency structure is complex, are sufficient design patterns
specified in the Development view to guide its implementation?
Does the codeline defined in the Development view support the packag-
ing of the system’s functional elements into the operating system pro-
cesses specified by the Concurrency view?
Does the test approach defined in the Development view support testing
of the concurrency structure specified in the Concurrency view?
Does the development environment defined in the Development view
allow development and testing of the concurrency structure specified in
the Concurrency view?
AND D EPLOYMENT V IEW C ONSISTENCY
Goal: To ensure that the system’s runtime tasks are correctly mapped to
execution resources.
Is every operating system process mapped to a processing node to allow
it to run?
Can the interprocess communication facilities used in the Concurrency
view be implemented on and between the processing nodes specified in
the Deployment view?
434
 P A R T III  A V I E W P O I N T C A T A L O G
Are the processing nodes specified in the Deployment view sufficiently
powerful to host the processes mapped to them from the Concurrency
view?
Is every processing node in the Deployment view fully used by the pro-
cesses mapped to it?
D EPLOYMENT AND O PERATIONAL V IEW C ONSISTENCY
Goal: To ensure that the deployment environment described in the Deploy-
ment view can be installed, used, monitored, managed, and supported.
Does the Operational view define how each of the elements in the
deployment environment will be installed?
Does the Operational view describe how each of the elements in the
deployment environment can be monitored and controlled?
Does the Operational view make it clear which monitoring and control
facilities already exist, which can be bought, and which must be devel-
oped?
Can each of the elements in the deployment environment be supported in
the organization?