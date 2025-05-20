18
T HE I NFORMATION
V IEWPOINT
Definition
Concerns
Models
Problems and
Pitfalls
Stakeholders
Applicability
Describes the way that the system stores, manipulates, manages,
and distributes information
Information structure and content; information purpose and usage;
information ownership; enterprise-owned information; identifiers
and mappings; volatility of information semantics; information stor-
age models; information flow; information consistency; information
quality; timeliness, latency, and age; and archiving and information
retention
Static information structure models, information flow models, infor-
mation lifecycle models, information ownership models, information
quality analysis, metadata models, and volumetric models
Representation incompatibilities, unavoidable multiple updaters,
key-matching deficiencies, interface complexity, overloaded central
database, inconsistent distributed databases, poor information qual-
ity, excessive information latency, and inadequate volumetrics
Primarily users, acquirers, developers, testers, and maintainers, but
most stakeholders have some level of interest
Any system that has more than trivial information management
needs
The ultimate purpose of any information system is to manipulate data in
some form. This data may be stored persistently in a database management
system, in ordinary files, or in some other storage medium such as flash
memory; or it may be transiently manipulated in memory while a program
executes.
293
294
C ONCERNS
P A R T III  A V I E W P O I N T C A T A L O G
Nowadays, many organizations possess massive amounts of information
on their customers, their products or services, their own internal processes, and
their competitors. Although some of this information may be hard to access,
inconsistent, and inaccurate, it still represents a substantial asset—one that, if
correctly used, can bring substantial benefits. We see this often in large sys-
tems integration projects that attempt to bring together information from a vari-
ety of sources to produce a consolidated customer view, an integrated view of
the supply chain, or an accurate financial picture.
Formal data modeling and design can be a long and complex process. As an
architect, you can do data modeling only at an architecturally significant level of
detail. You need to focus on those aspects of the data model where getting it wrong
would affect the system as a whole rather than just a part of it. Your task is to
develop a summary view of static information structure and dynamic information
flow, with the objective of answering the architecturally significant questions
around ownership, latency, relationships and identifiers, and so forth.
You use the Information view to answer, at an architectural level, questions
about how your system will store, manipulate, manage, and distribute information.
Information Structure and Content
The structure and content of the information that your system manages are
clearly significant concerns. Your challenge as an architect is to focus on the
most important aspects of information structure, those that have system-wide
impact, and to leave most of the modeling and decision making to the data
modelers and data designers.
You should focus on a relatively small number of data items (entities,
classes, and so on) and the relationships among them. Deciding which data
items are important depends on the problems you are trying to solve and the
concerns of your stakeholders. However, you should bear the following in
mind when selecting the data items of interest.
Focus on a small number of data items that are core to the primary
responsibilities of your system or that your stakeholders view as particu-
larly significant or meaningful. When considering the interests of the
stakeholders, primarily consider your users, but also take into account the
concerns of other stakeholder types such as maintainers.
Focus on information-rich data items, rather than ones that have few
attributes (e.g., type entities are typically less important in architectural
information models). Choose data items that:
• Are fundamental to the nature of the concerns being addressed
• Are significant to the users or other stakeholders
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 295
• Have a complex or poorly understood internal structure
• Can have a significant impact on the system’s quality properties,
depending on how they are represented
• Are heavily used or volatile (the contents are expected to change
frequently)
In the early stages of developing your models, try to focus on abstract
rather than physical information, and keep the models simple. Don’t
worry too much about formal modeling techniques such as relational
normalization at this point.
Your early models should typically align with and be driven by your
system’s functionality, and you should be concerned less with physical
considerations such as location or ownership (although we address these
issues and others in this chapter).
Information Purpose and Usage
Information can be used in different ways—to support operational processes,
such as taking an order or making a payment; to present current operational sta-
tus, such as stock levels or production rates; or to analyze historical information
and uncover trends and patterns. While it is the same information in each case,
the distinction is important in the design of information systems, since the dif-
ferent usage patterns often have significantly different information ownership
rules and may require significantly different architectural solutions.
Most information systems have at their heart a transaction store or
online transactional processing (OLTP) database. The transaction store
manages the information required to support day-to-day operational
business processes. This information is highly volatile, and the system
needs to be able to process a large number of concurrent read and write
operations with short latency and high reliability.
If the system has significant reporting requirements, this can put a severe
strain on the transaction store. A long-running or complex query can dis-
rupt access by operational users, leading to increased response times and
lower throughput. For this reason, some systems implement a separate
reporting database to service these large queries, which is fed in batch or
real time from the transaction store. The reporting database is essentially
read-only (apart from the incoming information feeds) and is optimized
for complex ad hoc queries rather than updates, with many indexes and
significant denormalization.
The transaction store and reporting database usually store only information
related to current activity, such as open orders, current stock levels, or today’s
prices. Some users require access to historical information, to look at individ-
ual transactions or to analyze and summarize the information in different
296
 P A R T III  A V I E W P O I N T C A T A L O G
ways. Historical information is usually managed in a separate data ware-
house, sometimes called an online analytical processing (OLAP) data store.
The data warehouse may in turn feed into more specialized data marts, which
manage information from a specific domain or time period. The data ware-
house holds a record of all activity going back many years and can be used to
retrieve specific historical information or to analyze trends over time.
 Most systems rely heavily on reference data (sometimes known as static,
master, or lookup data), which is the information on people, places, and
things that categorizes or classifies the system’s transactional informa-
tion. It includes a wide range of business entities, such as calendars,
customers, products, parts and supplies, prices, locations, employees, and
external organizations. It also includes the “type” information (such as
product type or employee role) that characterizes other information. Every
organization has its own definition of what it classes as reference data,
but it is almost always fairly static, changing relatively infrequently, and
there is usually much less of it compared with transactional and opera-
tional information. As we will see shortly, reference data may not be
owned by your system, which can be a significant architectural challenge.
While the distinction here may not be important in the early days of an infor-
mation system, over time the system will amass larger and larger volumes of data.
It will be much easier to hive off a separate reporting database, data warehouse,
or enterprise data store in the future if the initial architectural design has taken
this possibility into account and allowed for the impact of partitioning, speeds of
different stores, data duplication between stores, and so on.
Information Ownership
In many architectures, particularly those that involve the integration of new
and/or existing systems, information is physically distributed across multiple
data stores and accessed in different ways. This situation, while often un-
avoidable, creates all sorts of problems.
Which copy of a particular data item is the most up-to-date one?
How do you keep synchronized any information held in multiple places?
How do you deal with information that is derived from information
managed and owned elsewhere, such as account balances derived from
account activity?
What validation and business logic should be applied to the modification
of data items, and what assumptions can be made about data items that
have been validated elsewhere?
If the same data item can be modified in several places, how are conflicts
reconciled?
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 297
EXAMPLE An insurance company employs a large number of workers
who visit customers at home to sell them financial products. The com-
pany maintains a central database of customers and prospects, an ex-
tract of which is downloaded to each salesperson’s laptop when visiting
the office. Whenever a sale is closed at a customer’s home, the informa-
tion is stored in a holding area on that laptop until it can be uploaded to
the central database later.
The company opens a call center that allows customers to update their
details and also offers limited capabilities to sell products. This leads to
an increase in the number of complaints for various reasons. Some-
times, details stored on laptops overwrite more recent data on the central
database, and vice versa. In other cases, updates to the central database
are rejected because they fail the central system’s more stringent valida-
tion.
In order to address these problems, the architect first has to agree
with the business stakeholders on some general rules about how to deal
with update conflicts and failures (e.g., recent updates always override
older ones). These rules are then coded into the central system and
laptop applications.
A useful way to analyze these problems and develop architectural strategies
to handle them is to develop a model of information ownership. The information
owner (or master) of a data item is the system or data store that contains the
definitive, up-to-date, validated value of that data item. The information owner
always has the correct value for that information and can act as the final arbiter
when any disputes over accuracy occur.
By defining the owner of each data item, you can ensure that your infor-
mation consumers are always working with the right information and that
your information producers write it only to the correct place. When this is not
possible in practice, you can analyze potential conflicts and inconsistencies
and then develop strategies to deal with them.
EXAMPLE A national system for registering motor vehicles operates
from a number of semiautonomous regional centers. Each center is re-
sponsible for registering vehicles purchased in that region. Each vehicle
must be allocated a unique number, but conflicts could arise because
there is no real-time communication between the regional centers. (In
information ownership terms, each center is a creator of the vehicle reg-
istration number data item.)
298
 P A R T III  A V I E W P O I N T C A T A L O G
The problem is resolved by partitioning the information ownership,
that is, by allocating to each center a separate, distinct range of numbers
to assign to vehicles purchased in its area. Care must be taken to ensure
that the ranges will never overlap. This is done by making each range far
larger than the anticipated number of cars to be registered: The North
center is given the range 1 to 100 million, the West center 101 million to
200 million, and so on.
A by-product, incidentally, of your information ownership analysis will
be a high-level definition of some of your system’s interfaces. Where one sys-
tem is an information owner and another is an information consumer (or
maintains a copy of that information), some sort of interface is required be-
tween them. You can use the interface definitions to cross-check the models
in your Information view against the models in your Functional view. Any in-
terface derived from information ownership rules should also exist as a pro-
cess flow between the two participants.
Enterprise-Owned Information
Nowadays many large organizations maintain “enterprise” sources of important
information, and you are usually required to use them rather than owning and
managing such information yourself. Enterprise information is usually highly
valuable to the organization, and the consequences (to you, and to the organiza-
tion as a whole) of it being incorrect or out-of-date are severe.
The most common form of enterprise information is enterprise reference
data. (As we described earlier, reference data is the information on people,
places, and things that categorizes or classifies your system’s transactional in-
formation.) This may be general-purpose information, such as country codes or
currencies, or it may be specific to your organization, such as products, suppli-
ers, or customers. You may also need to make use of more volatile enterprise
information, such as end-of-day stock levels or account balances.
Your system may be expected to access enterprise information directly
from the source system when it needs it, or it may be required to maintain
its own copy that is refreshed regularly in real time or batch. In some cases
your system may also need to update the enterprise information itself, using
standard mechanisms and business processes defined by the information
owner.
In any case, the enterprise information your system uses must be
accurate, up-to-date, consistent, and complete. There are several ways this
can be achieved, each of which has implications for users as well as for the
architecture.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 299
EXAMPLE A travel agency has branches across the country and also
sells directly to customers over the Internet and from a call center. The
travel agency has started a customer affinity program and wants to
build a system to make holiday recommendations to select customers
based on their preferences, budgets, and travel history. The system will
make use of various types of enterprise reference information, including
details of holiday destinations, tour operators, airlines, and hotels. In
addition, it will use more volatile enterprise information on standard
pricing plans and special offers.
All of this enterprise reference information is held in central data
repositories but needs to be managed in different ways. Information on
holiday destinations, airlines, and tour operators changes rarely, and a
copy can be downloaded to the system’s own database weekly. Hotel
information and list prices are more volatile, and an overnight extract is
required. Special offers arise at short notice, and a “semi-real-time” feed
of these is needed (in reality, a small batch extract that runs at regular
intervals during the day).
Affinity customers sometimes like to suggest hotels they have used
in the past but are not on the travel agency’s database. In this case the
system needs to be able to upload the hotel details to the enterprise
store, and after some validation these should be added so that they are
available for other systems to use.
As discussed elsewhere in this chapter, each of these different access mod-
els has its advantages but also may lead to problems. Data that is refreshed on
an overnight batch schedule may be out-of-date when it is used. Obtaining data
in real time mitigates this problem but is more complicated to implement and
manage. Accessing a single central repository ensures that data is always up-to-
date, but the repository becomes a bottleneck and a single point of failure, and it
may not be feasible to do this for systems that are geographically dispersed.
We address some of these concerns further in our discussion of the Location
perspective in Chapter 29.
Identifiers and Mappings
Whether information is managed by using relational entities or objects and
classes, each data item needs a unique identifier or key that distinguishes it
from others of similar type (e.g., customer number, machine serial number, or
ISBN). In relational database terminology, this is called a primary key; in object-
oriented programming, the term object ID is often used; a more useful general
term (which does not assume any underlying information model) is identifier.
300
 P A R T III  A V I E W P O I N T C A T A L O G
When information is spread over multiple repositories, identifiers often
become an issue. Different systems may use different mechanisms to identify
the same data item, and these mechanisms will need to be reconciled at points
where data exchanges occur. Because key assignment can be a volatile activ-
ity (consider a sales system where many new orders are created per second),
you will need to keep this reconciliation process up-to-date with new infor-
mation as it arrives.
EXAMPLE A newspaper captures sports information submitted by jour-
nalists along with results and scores that arrive electronically. The paper
collates the information and publishes daily league tables for individual
competitors and teams. Although the paper’s own central database allo-
cates identifiers to each competitor and team, most of the information
sources refer to them only by name—and in the case of foreign competi-
tors, these names are not always spelled correctly.
The database is suffering some significant information quality issues.
Scores and results are sometimes allocated to the wrong player or team,
phantom teams with spellings similar to real ones are created regularly,
siblings’ results are often allocated to the wrong person, and some results
fail to be loaded at all.
Problems like these can often be only partially addressed by architectural ca-
pabilities and features. Defining standard identifiers for teams and players in
this example will help, but business process changes will also be required to en-
sure that users of the system carefully map names to their correct identifier—
perhaps by being required to pick names from a drop-down list rather than type
them in directly. However, imposing rules like these can make a system awkward
to use, and you should collaborate carefully with your business stakeholders to
come up with a solution that is both usable and effective (perhaps using an ex-
ception workflow to confirm the correctness of automatically matched identifi-
ers, allowing partial automation with manual input to ensure data quality).
There are many other architectural challenges associated with the use of
identifiers. For example, identifiers are normally invariant, that is, they never
change over the lifetime of the data entity that they identify. However, it is
not always possible to enforce this rule. In such cases, the mechanisms (and
business processes) for creating and changing identifiers must be very care-
fully specified and designed.
There can also be some subtleties around the question of whether two
data entities actually represent the same thing and should therefore have the
same identifier. For example, every book is allocated an ISBN (International
Standard Book Number) when it is published. A second edition of the book
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 301
EXAMPLE Derivatives are financial products whose value is derived
from the value of some other underlying asset. For example, a share
option gives the purchaser the right, but not the obligation, to buy an
agreed-upon number of shares at an agreed-upon price at an agreed-
upon date in the future. The derivatives market is constantly changing,
with new and more complex products being introduced all the time.
When a new derivative product is created, it goes through an approval
process to ensure that it is sound, that it is compliant with regulations, and
that its financial parameters are clear. This process can take a relatively
long time, and in the interim it is common for the product to be allocated a
temporary identifier so that a provisional price can be quoted and measures
of value and risk can be calculated. Once the product is formally approved,
it is given a permanent identifier, which may be different from the tempo-
rary one since it is allocated by a different part of the organization.
A link must be established between the two identifiers, so that the
provisional quote can be turned into a firm quote and a sale made with a
clear audit trail.
may contain only minor revisions and corrections, or may be substantially
different, with a new structure and a substantial amount of new content.
Should such a major revision be allocated a new ISBN? If so, how can it be
linked to the ISBN of the first edition? If not, how are the two editions distin-
guished from one another? In this example, there are agreed-upon rules
about allocating ISBNs, but in many cases it will be down to the architect to
decide (or at least capture and agree on the requirements from users).
Another important consideration is whether your identifiers are going to be
user-visible or not. For example, every debit and credit card has a unique 16-digit
card number that the cardholder uses when making a purchase online or over the
telephone. On the other hand, although each individual purchase on a credit card
statement has its own identifier, this is not usually printed. If a transaction needs
to be queried or confirmed, it is identified by the transaction date, the merchant
name, and the amount (which is usually unique enough for this purpose).
Volatility of Information Semantics
It is common nowadays for the syntax, semantics, and interrelationships of busi-
ness information to undergo frequent and unpredictable change. New fields may
need to be added to existing entities, new constraints and relationships may
arise, or new types of entities may be needed to meet changing business needs.
Although there are mitigation strategies to make such changes less pain-
ful (including abstract database access libraries, tools for impact analysis, and
302
 P A R T III  A V I E W P O I N T C A T A L O G
designing interfaces to allow for variation and change), even small changes to
an information model can have wide-ranging implications for the systems
that use that information. For example, if a new mandatory field is added to a
database table, every process that creates or updates rows in that table needs
to be changed so that it can provide a value for that field. This process needs
some form of control, traditionally managed through a formal process of data
model change control: The impact of a change on every module in the system
is assessed, and only when all parties have implemented the required func-
tional changes is the database change rolled out.
This approach is established and effective, but it drastically slows down the
rate at which systems can be changed, and in practice change control often ends
up being subverted or bypassed altogether. An alternative approach, which is
more flexible while still retaining a level of control, is to decouple the information
semantics from the physical structures used to store it. A common way of doing
this is to store complex information structures in structured text forms such as
XML, JSON, or YAML, either within a database or in external data files. With a
disciplined approach, and the possibilities that exist today for automation, you
can also take a more dynamic and flexible approach to changing a database
schema, as proposed by the Evolutionary Database Design technique (see Fur-
ther Reading for more details).
The XML family of data management standards includes mature mecha-
nisms for defining the schemas of XML documents and accessing their con-
tents. While changes to the schema still need management and oversight, they
can often be implemented more quickly with less effort. The downside of this
approach is that XML-based systems tend to be less performant and scalable,
due to the XML management overhead and the fact that most database optimiz-
ers don’t work very well with XML data.
Information Storage Models
The third-normal-form relational database is so dominant in enterprise infor-
mation systems that it can be easy to forget that there are other approaches
available for storing information. The following four major types of informa-
tion stores are all in wide use today.
Relational databases dominate the enterprise information systems land-
scape and need little introduction. A typical relational database contains
a largely third-normal-form schema and is usually used as some form of
transactional or operational data store. Relational databases are usually
implemented using a third-party database management system and allow
data retrieval and manipulation operations to be expressed in a declarative
form using the SQL language. They typically enforce data integrity via an
ACID transaction model (meaning that database transactions are used to
ensure that updates are Atomic, Consistent, Isolated, and Durable—hence
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 303
ACID). Well-designed relational databases avoid data duplication (via nor-
malization), are flexible (due to the ability to write queries in an uncon-
strained manner across the data model), can provide good performance
and scalability characteristics, and are relatively easy to use for small and
midsize problems. The limitations of a relational database tend to be the
difficulty of scaling them to very large problems and the complexity of the
schema and queries that often results when implementing a large enter-
prise application.
 Dimensional databases are another storage model based on the relational
storage model and can be implemented using standard relational data-
base engines, although specialized column-based or dimensional stores
are often used instead. Rather than using a third-normal-form schema, a
dimensional store is based around a multidimensional (or “star”)
schema model, with large “fact” tables containing the primary data in the
database, linked to small “dimension” tables that contain classification
data that can be used to group and summarize the fact data. (We de-
scribe multidimensional schemas in the Static Information Structure
Models section later in this chapter). Dimensional databases are particu-
larly well suited for complicated reporting problems, and so this storage
model is often used for reporting databases rather than transactional da-
tabases. The major limitation of a dimensional model is the relative diffi-
culty of updating information after it has been added to the database.
 NoSQL databases are a relatively recent development and at the time of
writing are still fairly rare in mainstream enterprise systems, but they
have proved their usefulness in many very large-scale Internet services
for e-commerce, Internet search, and social networking. 1 There are many
data storage technologies that classify themselves as “NoSQL” products,
and each one has its own unique characteristics, strengths, and weak-
nesses. What is common among the NoSQL products is the fundamental
tradeoff they have made, which is to abandon the traditional RDBMS
characteristics of strict tabular data storage and SQL-query-based data
access (and in some cases ACID transaction semantics) in order to
achieve simplicity and very high scalability and performance. Most of
these databases are accessed via a simple “map”-based interface that
allows records to be stored and retrieved by key, sometimes also offering
simple query facilities based on the attributes of the records being
1. In fact, the very first commercial database management systems were network and hier-
archical databases, which also didn’t use SQL. Since then, object-oriented databases, which
also don’t use SQL, have come and gone too. Here we’re referring to the more recent data-
base technologies aimed at solving very large, distributed data management problems that
have been developed primarily to meet the demands of Internet-scale systems.
304
 P A R T III  A V I E W P O I N T C A T A L O G
retrieved. This simpler model of data storage allows the database engine
to be distributed across a very large number of servers, a configuration
that provides good performance and a high degree of scalability. Of
course, if you decide that you need a rich strongly typed database, or a
powerful query-processing engine, these technologies are less suitable.
File-based stores shouldn’t be forgotten either, and even today, a surpris-
ing amount of enterprise data can usually be found stored in flat files.
Files have the benefits of simplicity and ubiquity and, for some situations,
the best performance too. They are particularly well suited to “write-once”
requirements such as logging and auditing. Nearly every technology can
read and write files directly, and there are a number of simple query
engines that can be used with flat files to simulate a database. Of course,
the simplicity of flat files can also make them unsuitable for many
demanding tasks, where complicated queries, reliable transactional
updates, or complicated data structures make the use of files difficult.
As an architect, you need a good awareness of the different information
storage models available to you, and you should carefully consider the needs
of your system so that you can match the right sort of storage model to your
data storage requirements.
Information Flow
Just as important as the static information structure is the way that informa-
tion moves around the system and is accessed and modified by its elements.
The important questions here include the following.
Where is data created and destroyed?
Where is data accessed, modified, and enriched?
How do individual data items change as they move around the system?
As with information structure, it is usually necessary to consider only the
most important information flows as part of architecture definition, that is,
those that are crucial to the system’s primary responsibilities or those that
will have a material impact on its quality properties. In any case, because you
will have only a high-level data model to work with, you won’t be able to drill
down into too much detail here.
Because the main purpose of most systems is to process information, in-
formation flow is often analyzed within Functional rather than Information
views. This works well as long as you don’t end up with a small number of
complex, overloaded models that are hard to understand—and as long as you
make sure that the data-specific concerns discussed in this chapter are also
addressed.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 305
Information Consistency
Information consistency means that information held in different parts of the
system, or in different but related data items, should be compatible, congruent,
and not in conflict. This may be as simple as a referential integrity constraint
(e.g., if a customer is recorded as owning several products of specific types,
these products should all exist) or may be more subtle and complex (e.g., a
summary financial position should always match the underlying data used to
calculate it). Most businesses have sophisticated rules for information consis-
tency, although it is rare in our experience for these to be written down anywhere.
Information consistency is so fundamental to the operation of modern
relational databases that its significance in the architectural context can
easily be forgotten. A classic example, which we repeat here, illustrates its
importance.
EXAMPLE A bank customer uses an automated teller machine to trans-
fer $500 from her checking account to her savings account.
The bank uses two data stores, CHECKING and DEPOSIT, to manage
these two different types of accounts. The transfer is implemented as
two updates: a withdrawal of $500 from CHECKING, and a corresponding
deposit of $500 into DEPOSIT, as shown in Figure 18–1.
It’s essential that either both of these updates complete successfully
or neither of them do. For example, the transaction might not go ahead
if the customer doesn’t have sufficient funds in her checking account. If
only one of the transactions completes, either the customer or the bank
would lose money.
306
 P A R T III  A V I E W P O I N T C A T A L O G
A transaction is a sequence of data updates that occur as an atomic unit—
that is, either all updates are accepted and written to permanent storage or
none of them are. Transaction management ensures the right outcome by
committing updates (writing them permanently to disk) only if all updates can
be successfully applied. Transaction management will roll back (undo) all of
the updates if one of them fails.
Transaction management features are provided by all modern relational
database systems, and their use is nowadays almost automatic (although care
must be taken to avoid pitfalls such as heavy contention or deadlocking).
Transaction management across multiple systems is much more complicated
to design, build, and operate, requiring complex techniques such as two-
phase commit. Such techniques can impose a heavy burden on processing
power, leading to increasing latency and response time, and you should use
them only when absolutely necessary.
An alternative approach that avoids some of the difficulties with distrib-
uted transactions is to use compensating transactions to maintain data
integrity. In this model, each data update is committed individually, and if a
later update fails, each committed update is reversed by a transaction with an
equal and opposite effect to the original one. In the preceding example, if the
withdrawal was successful but the deposit failed, a compensating deposit of
$500 to the checking account could be applied to bring everything back to a
consistent state.
Compensating transactions often work better in practice than two-phase
commit since they do not require database locks to be held over separate data
stores at the same time. However, they have problems of their own, particu-
larly if changes cannot easily be reversed or if a compensating transaction
itself fails.
Another approach is known as eventual consistency. In this model, distrib-
uted applications favor high availability over consistency and are designed to
be able to cope with data that is out of synch for a period of time. Such a system
guarantees that after an update, all instances of the same data will eventually
be updated to this value, without guaranteeing how long this will take.
Eventual consistency is used for infrastructure software such as DNS (the
Internet’s Domain Name Service) and for some Internet-scale applications such
as global search engines, e-commerce sites, and social networking sites, but the
principles may also be useful to smaller-scale applications. The model is some-
times referred to as following BASE principles (Basically Available, Soft state,
Eventual consistency) in contrast to traditional transaction management, which
is referred to as ACID (because, as we noted previously, the transactions are
Atomic, Durable, Isolated, and ensure that data is Consistent).
We also discuss the application of this technique as a way of scaling to
very large data volumes in Chapter 26 and its relevance to achieving high sys-
tem availability in Chapter 27.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 307
Information Quality
The quality of a particular data item is the extent to which the current value
of that data item agrees with the correct value in the real world. Poor-quality
information can have a significant impact on an organization’s ability to
carry out its operations. If you don’t have accurate information about your
customers, for example, you risk annoying them, losing them, or even being
sued by them. (Given all this, it is still a surprise how many systems man-
age to survive on information that is incomplete, incorrect, or outdated,
proving the old adage that something is often better than nothing, even if it
is imperfect.)
EXAMPLE A mail-order furniture company has created a marketing
database from customer orders and requests for brochures or quota-
tions. It uses this customer database to phone customers about special
offers and to try to persuade them to buy more of the company’s
products.
Unfortunately, the data in the marketing database has been cobbled
together from a number of sources and is therefore outdated and inaccu-
rate. Moreover, a number of customers have asked not to be cold-called,
but these requests have not always been transferred from the spread-
sheet where they’re managed into the marketing database.
As a result, many customers receive cold calls who do not want them,
or are offered products they already own, or are offered unsuitable
products (e.g., those that are too expensive). This creates a significant
amount of dissatisfaction among existing and potential customers, lead-
ing to bad publicity and possibly to lost sales.
Information quality becomes an issue for you as an architect in cases where
the system makes use of information from a variety of sources, particularly
when some of these are external to your sphere of influence. If your information
quality is variable, you must consider such issues as the following.
How will information quality be assessed and monitored (especially
when information is frequently updated)?
What minimum information quality criteria must be met?
How will these criteria be enforced?
How will poor-quality information be improved? Will this be done in an
automated way, or will it require manual intervention?
308
 P A R T III  A V I E W P O I N T C A T A L O G
Can good-quality information be corrupted by information of lesser qual-
ity? (For example, a customer address is updated, but the postal code is
omitted.) If so, should this be prevented or checked?
Is it possible for information quality to degrade as it flows around the
system?
The answers to these questions are likely to have implications for your
architecture. For example, it may be necessary to develop or deploy automated
tools for monitoring or assessing information quality or for repairing poor-
quality data. If repairing data needs some human intervention, you may have
to set up a holding area where data can sit until it has been manually repaired.
It is becoming more common to use workflow to address information
quality problems when repair processes cannot easily be automated. In this
model, a list of tasks, such as correcting a customer’s name or address or
dealing with a suspect transaction, is managed in a central database. Tasks
are assigned to users and the system tracks their status to completion. Tasks
can either be standardized (defined at design time) or, in the most sophisti-
cated workflow systems, ad hoc (created by someone at runtime). Service lev-
els may be defined that commit the company to fixing problems within a
certain time or at a certain rate.
If well designed, this approach can be an effective way of improving in-
formation quality and customer satisfaction.
Timeliness, Latency, and Age
If your information is held in a single data store and always accessed synchro-
nously in real time, timeliness, latency, and age may not be significant issues.
Unfortunately, many systems do not work this way, and it is inevitable that
some scenarios involve information that is old or out-of-date, if only by a few
minutes.
EXAMPLE A commodity brokerage accepts a number of feeds from
information sources that provide up-to-date pricing and volume informa-
tion, as well as news stories relevant to the commodities being traded. The
feeds are all channeled through a single gateway application that sorts, fil-
ters, and distributes the information to appropriate subscribers.
A catastrophic hardware failure renders the gateway unavailable for
several days. When it comes back online, the subscribers are flooded
with several thousand cached price messages that, because they are
several days old, are of no interest to the recipients.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 309
The gateway is modified so that after a failure, it discards cached
price messages that are older than a certain configurable age. Another
failure occurs (a change of hardware supplier is called for), and recovery
is much faster.
In this example we have separate information providers (the external
systems that provide pricing and volume information) and information consum-
ers (the internal users who make use of it). Because the process of information
transfer from provider to consumer takes a finite (and possibly long) time,
discrepancies can occur. If the time lag cannot be reduced to close to zero, you
need to work with stakeholders to develop solutions to the problems that may
arise from inconsistent information.
The time lag between the visibility of information to providers and to con-
sumers is expressed by means of latency, the length of time between a data
item being updated at the data source and the updated value being available
to all parts of the system.
You may also need to take into account the age of some data items (the
time since the data item was last updated by its data source). A system that
disseminates information on volatile stock prices, or the physical location of
trucks, for example, may not be interested in information that is hours or
even minutes old. You may be able to discard this information because it is no
longer needed.
You should identify key points where time-based inconsistencies can
arise and, with the help of your stakeholders, develop strategies to handle
them, such as the following.
Tag important data items with a “last updated” date and time.
Define “currency windows” for significant data items.
Warn users when information may be outdated.
Hide or discard information that may be too old.
Reduce latency by means of faster interfaces or direct access to data
sources.
Archiving and Information Retention
In many systems, it is becoming rare for information to be deleted; it may be
kept for legal reasons or for historical analysis. Although disk storage is now
relatively inexpensive, managing large databases is a complex process and
even enterprise disk architectures cannot expand indefinitely, so sooner or
later your information will grow to a point where it is not desirable to keep it
310
 P A R T III  A V I E W P O I N T C A T A L O G
all online. Then you will need to archive older, less useful information to
some other storage medium such as high-capacity offline storage.
You must define carefully the scope of information to archive. It obviously
can’t be information that is still needed to support any production activities,
nor should it be information that is likely to be useful for regular analysis. In-
formation is usually selected on the basis of age combined with business
rules to determine its usefulness.
Your archiving strategy can have a significant impact on your architecture.
Archiving large volumes of information may make some systems fully or
partly unavailable for significant periods of time.
Your physical disk sizing needs to take into account the length of time
that information will be retained.
You may need to define the processes that move production information
to archive media.
You may need to take special actions to ensure the integrity and consis-
tency of the production and archive storage.
There may be an impact on the network infrastructure if archive storage
is remote.
Don’t try to add archival capabilities as an afterthought. Design your
architecture from the beginning in such a way that archiving is a natural part
of the information lifecycle.
Stakeholder Concerns
Typical stakeholder concerns for the Information viewpoint include those
listed in Table 18–1.
TABLE 18–1 S TAKEHOLDER C ONCERNS FOR THE I NFORMATION VIEWPOINT
StakeholderAcquirers
Class
Assessors
Communicators
Concerns
Concerned with preserving and safeguarding the value of the organiza-
tion’s information assets, so the following are key (although not always
recognized as such):
• Information quality and archiving
• Reference data
• Information retention
Interested in all aspects, with a focus on information structure and flow,
identifiers and mappings, and information quality
Rarely focus on detail on the information architecture, but may find a
background understanding of the key principles and strategies helpful
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 311
TABLE 18–1 S TAKEHOLDERStakeholder Class
Developers and main-
tainers
System administrators
Testers
Users
CONCERNS FOR THE I NFORMATION VIEWPOINT (C ONTINUED)
Concerns
Interested in how the architect’s models will translate into real databases
and (real-time, batch) information interfaces, and implementation details
such as how the data structures will support the required processing and
how consistency will be guaranteed
Interested in how these real-world system components will be managed
and supported
Interested in the main database structures, how they are affected by the
operation of the system, the data flow through the system, and how to
create realistic test data sets
Concerned with functional aspects of the information architecture (e.g.,
information ownership and regulation) and user-visible qualities such
as timeliness, latency, and age; and information quality
M ODELS
Data modeling is probably the best-served area of information systems in
terms of established, rigorous, and generally understood analysis and model-
ing techniques. The three most important types of models are the following:
1. Static information structure models, which analyze the static structure of
the information
2. Information flow models, which analyze the dynamic movement of infor-
mation between elements of the system and the outside world
3. Information lifecycle models, which analyze the way information changes
over time
We discuss these models in this section—particularly how they are used in
the architectural context—and briefly describe some other types of models
you may find useful, such as information ownership models, information
quality analyses, metadata models, and volumetrics models.
Static Information Structure Models
Static information structure models analyze the static structure of the infor-
mation: the important data elements and the relationships among them.
Entity-relationship modeling is an established technique of data analysis
that is based on a solid underlying mathematical model. Data items of interest
312
 P A R T III  A V I E W P O I N T C A T A L O G
are referred to as entities, and their constituent parts are called attributes. The
information semantics defines the static relationships among entities. Each
relationship has a cardinality, which defines how many instances of one of
the entities can be related to an instance of the other.
EXAMPLE A library stores a number of books for its members. Mem-
bers check out books for a period of time, after which they are renewed
or returned. Each book has one or more authors, who receive a fee
each time a book is checked out. The fee is paid to the author via the
book’s publisher.
Each of the italicized terms in this description is represented as an
entity in the entity-relationship model. Attributes of the model include
book title, author name, ISBN number, and publisher name and address.
Class models perform a role similar to that of entity-relationship models but
for the object-oriented world. They model data items ( classes), their constituent
data parts (attributes), and the static relationships among them (associations).
It is possible to use class model notation to model relational entities by omitting
the behavioral aspects from the model and limiting the association types (e.g.,
no generalization or composition).
Class models can also document the behavioral aspects of a system, such
as interfaces and methods, and features specific to object-oriented analysis,
such as inheritance.
EXAMPLE In the previous example, classes would be modeled for
books, members, authors, and publishers. Methods would provide the
necessary functionality for checking out books.
NOTATION There are a number of similar notation styles for documenting
entity-relationship models. Figure 18–2 shows an entity-relationship diagram
in the crow’s foot style for the library example.
A UML class model for the same example would look something like
Figure 18–3.
Data warehouses and data marts are usually modeled using more spe-
cialized semantics called a star schema (also known as a multidimensional
schema or cube). A star schema consists of fact tables, which contain nu-
merical data or other “facts” aggregated at many different levels and have
large compound keys. Clustered around each fact table are a number of
dimension tables, which model the different levels at which information can
be aggregated. The chief advantage of using a star schema is that an aggre-
FIGURE 18–2 ENTITY-R ELATIONSHIP D IAGRAM FOR THE L IBRARY E XAMPLE
gated value can be retrieved in a single database read, rather than querying
and summing all the underlying transactions. A snowflake schema extends
this model by normalizing the dimension tables into a hierarchical structure.
An example star schema for the library system is given in Figure 18–4
(although in practice a library management system is unlikely to need to
manage the sort of volumes that would necessitate a data warehouse).
FIGURE 18–4 S TAR S CHEMA E NTITY -R ELATIONSHIP D IAGRAM FOR THE LIBRARY E XAMPLE
ACTIVITIES Formal information modeling includes a wide range of activities.
The first step is to identify the important data entities. This is usually
done by inspecting the business processes and use cases for nouns such
as customer, product, payment, or event. In an architectural description,
you should focus on a small number of important entities (for example,
anything with a “type” in its name can usually be ignored).
A process called normalization reduces the model to its purest form, in
which there is no repeated, redundant, or duplicated information. It is
rare for relational models to be taken beyond third-normal form, and
from the architect’s perspective it is often more useful (although less
rigorous) to model some information unnormalized.
Domain analysis looks at attributes (fields) of data items and the rules
that define their permissible values. For example, a customer number
may always be a ten-digit integer with the last digit being a check digit,
or a telephone number is always a country code followed by a dialing
code and a number. Domain analysis is important in schema design but
is usually too detailed for an AD.
Techniques such as structural decomposition or aggregation are used to
derive class models. Structural decomposition involves breaking an
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 315
element into smaller coherent pieces, while aggregation is the reverse
process—creating a new element by combining other, similar elements.
Unfortunately, static information structure models are not easily decomposed
into levels of detail—for entity-relationship diagrams in particular, it is, in theory,
“all or nothing.” In practice, you do not have time to produce a hundred- or
maybe thousand-entity information model as part of your architecture. The way
to approach this is to focus on a small number of the most important entities/
classes and the relationships among them.
You can usually omit from your model detail such as intersection entities
(replace these with nonnormalized, many-to-many relationships, as we did in
the entity-relationship diagram shown in Figure 18–3 between author and
book) and type entities (such as product type).
As a very general guideline, if you have more than about 20 to 30
entities, or if your entity-relationship diagram won’t easily fit on a single
page, you have probably presented too much detail. In this case, you need to
either remove some less important entities from the model or use partitioning
and/or decomposition to simplify the overall picture.
Information Flow Models
Information flow models analyze dynamic movement of information between
elements of the system and the outside world.
These models identify the main architectural elements and the informa-
tion flows between them. Each flow represents some information transferred
from one component to another—in other words, an information interface.
Associated with each flow is a direction, the scope of the information trans-
ferred, volumetric information, and (in a physical model) the means whereby
information is exchanged, whether it is a transfer of flat files or a real-time
exchange of XML messages.
EXAMPLE A publisher supplies lists of newly published books to librar-
ies in a PDF document that is mailed to librarians monthly. When a
library receives a book, it is accompanied by an electronic delivery note
in the form of an XML file, which is imported directly into the library’s
book management system. When books are checked out and back in,
the new state is recorded by means of bar-code readers. When a book is
disposed of, it is manually marked as deleted in the system by a PC
application that accesses the database directly.
Each italicized term represents an information flow into, out of, or
around the system.
316
 P A R T III  A V I E W P O I N T C A T A L O G
As with static information modeling, you should aim to keep your informa-
tion flow models high-level and simple. It is not necessary to provide much de-
tail at the architectural stage. Fortunately, most notations support this naturally
through decomposition.
Information flow modeling is most useful for data-intensive systems, and
it complements the modeling of interfaces and function invocations in the
Functional view (see Chapter 17), which is often more appropriate to process-
ing-intensive systems. In practice, you usually do only one or the other, de-
pending on the nature of the system, the skills of the architect, and the
interests of the key stakeholders.
NOTATION There are a number of information flow notations from classic sys-
tems analysis, such as Gane and Sarson or SSADM data flow diagrams,
although these are as much about process as about information flow.
Figure 18–5 shows an example of a data flow diagram.
The following notation is used in the diagram.
Large rectangles represent processes that manipulate information.
Narrow open rectangles represent data stores (logical or physical
collections of information).
Arrows represent information flows.
Ellipses represent external entities (people or other systems that interact
with this system).
The diagram conveys several pieces of information.
Members and the librarian provide information to the checkout and
return processes.
A bookseller provides information to the acquire book process.
The librarian provides information to the dispose of process.
All this information is written to the BOOKS data store.
Information flow is usually represented in UML using activity diagrams,
which include the same sort of elements as shown in Figure 18–5.
ACTIVITIES Information flow models are typically created through a process
of stepwise refinement, with the most important flows being considered first
and then broken into further detail where necessary.
You can use your information ownership model, if you have one, to cross-
check against the information flows required to maintain information integrity
where ownership is distributed (as discussed earlier).
Information Lifecycle Models
Lifecycle models analyze the way information values change over time.
Entity life histories model the transitions that data items undergo in response
to external events, from creation through one or more updates to final deletion. A
life history can be a useful cross-check to ensure that there is processing to deal
with all of the life events associated with an entity. In particular, it can help you
ensure that entities are created in a controlled manner and that all entities have a
means of deletion.
EXAMPLE A book is created when it is published (as far as the library
system is concerned, anyway). The book is then acquired by the library
and repeatedly checked out and returned until it is finally disposed of.
Each italicized verb in this description is an event in an entity life
history for a book.
State transition models (or statecharts in UML terminology) model the
overall changes in a system element’s state in response to external stimuli.
This is a useful way to model systems whose interactions with the outside
318
 P A R T III  A V I E W P O I N T C A T A L O G
world cause their internal state to go through many transitions in seemingly
unpredictable ways. A statechart models a system element as a finite state
machine (FSM). An FSM always has a current state, which is the sum total of
the information it holds. When an external event occurs, the FSM changes
deterministically to another state and may also instigate some special pro-
cessing as a result of the change.
EXAMPLE A book is initially published; it is then acquired by the
library, and once on the shelves it alternates between being available
for loan and checked out, until it is disposed of.
Each italicized term represents a state of a book.
NOTATION An entity life history is usually represented by using some sort
of tree structure, with nodes for each event and branches to represent itera tion,
selection, and so forth, as shown in Figure 18–6.
A UML state diagram uses railroad tracks to represent the possible state
transitions of a book, as shown in Figure 18–7.
ACTIVITIES Lifecycle models are derived through an understanding of the
system’s functional requirements, by identifying all of the significant events
and understanding the information impact of each.
Other Types of Information Models
I NFORMATION OWNERSHIP M ODELS Information ownership models define
the owner for each data item in the architecture. In this context, “data item”
typically means entity (table) or, occasionally, attribute (field), although more
complex partitions can be modeled. Of course, in practice, life is never this
simple, and you may have to model a number of different classes of
information ownership, such as:
Owner or master, which holds the definitive value for that data item
Creator, which creates new instances of that data item
Updater, which modifies existing instances of that data item
Deleter, which deletes existing instances of that data item
Reader, which can read but not change instances of that data item
Copy, which holds a read-only copy of that data item
Validater, which performs validation on the data item to ensure that it
meets business rules
A combination of these
At its simplest level, information ownership can be modeled by using a
grid, with systems and data stores along one axis and data items along the
other. Each cell in the grid defines the ownership class of that data item, as
shown in Table 18–2.
It may be useful to develop a trust and permissions model to define which
systems, under which circumstances, are allowed to modify which data items.
For example, an external system that provides data updates in a weekly batch
might be trusted less than one managed and monitored internally, might require
further validation before updates are accepted, or might be constrained to updat-
TABLE 18–2 E XAMPLE OF AN I NFORMATION O WNERSHIP G RID
System
 Customer
 Product
 Order
 Fulfillment
Catalog
 None
 Owner
 None
 None
Purchasing
 Reader
 Updater
 Owner
 Creator
Delivery
 Copy
 Reader
 Reader
 Updater
Customer
 Owner
 Reader
 Reader
 Reader
320
 P A R T III  A V I E W P O I N T C A T A L O G
ing only noncritical data values. As well as being useful here, the definition of in-
formation ownership will be an important input to the process of securing the
system, as explained with regard to the Security perspective in Chapter 25.
In practice, you may not be able to avoid having more than one creator/
updater/deleter for a data item (although it is useful to try to define a single in-
formation owner). This particularly occurs when valuable information is held in
legacy systems. When two systems can modify the same piece of data, you need
to develop conflict resolution strategies, such as the following, to ensure that
business rules are followed and that information is left in a consistent state.
Always accept the latest update.
Maintain multiple copies of the same data item, tagged with their sources.
Maintain a history of data changes rather than just the latest version of
the data.
Trust one system more than another, so that system’s updates take priority.
Create more complex rules depending on the data changed and the nature
of the change.
Record multiple values and require manual intervention to fix the conflict.
Reject conflicting updates altogether.
Use a combination of these strategies.
With multiple updaters a particular problem is detecting that a conflict has
occurred. This can be addressed by stamping each record with an incrementing
version number and the date and time that the record was last updated.
Although you are unlikely to define detailed rules as part of your AD, it is
important to provide sufficient advice and guidance for your designers.
I NFORMATION QUALITY A NALYSIS From the architectural perspective, your
information quality analysis will focus on defining sources of poor-quality
information and principles and strategies for dealing with this information.
Possible strategies include the following.
Accept poor-quality information: This approach is suitable when poor-
quality information is not an issue or when the cost of repairing informa-
tion far outweighs the benefit of improving it.
EXAMPLE An Internet search engine manages a database of many hun-
dreds of millions of URLs. At any one time, a small proportion of these
will no longer be valid because pages have been renamed or Web sites
removed. However, it is not cost-effective for the search engine to regu-
larly clean up its database to remove these links.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 321
Automatically fix poor-quality information: There are a number of tools
available to do this, depending on the type of information.
EXAMPLE You can use tools that will repair or complete addresses or
telephone numbers, based on databases of postal codes or telephone
dialing rules.
Discard poor-quality information: This may be the best approach when
the cost of bad information far outweighs the cost of not having the
information at all.
EXAMPLE A company receives bulk mailing lists of variable quality from
an external supplier, which it uses to send out marketing material to
potential customers. For about 10% of the data, postal codes are missing,
invalid, or do not correspond to the mailing address. Such records are dis-
carded because the company is penalized by the postal service if too much
of its outgoing mail is incorrectly or incompletely addressed, and material
sent to these addresses is unlikely to arrive anyway.
 Repair poor-quality information manually (in other words, get users to
fix it): This is a very costly approach, however, and you must consider
how poor-quality information will be identified and how it will be
forwarded to users for correction.
Be aware that there may be legislative requirements for information qual-
ity (e.g., some countries charge penalties for maintaining or using incorrect
information on members of the public). We consider this point further in our
discussion of the Regulation perspective in Chapter 29.
M ETADATA MODELS Metadata is “data about data.” Metadata consists of
rules that describe and prescribe data items of interest—entities, attributes,
relationships, and so forth. Metadata originated in the study of geospatial
data and has had an increased profile in recent years following the growth of
the World Wide Web and various initiatives around business-to-business
communication.
ISO Standard 11197-3 defines metadata as “the information and documen-
tation which makes data sets understandable and sharable for users.” 2 Meta-
data may address a number of aspects of the information it describes, such as:
2. [ISO96], p. vii.
322
P ROBLEMSP A R T III  A V I E W P O I N T C A T A L O G
Data format (syntax)
Data meaning (semantics)
Data structure
Data context (the relationships among data items)
Data quality
Many organizations are beginning to develop enterprise-wide metadata
models; if these are available to you, they can form an extremely valuable
input to your Information view. In addition, a number of cross-industry meta-
data models are being developed under the auspices of groups like the Dublin
Core Metadata Initiative.
Metadata models are closely allied to the other types of information
models we have described, particularly information structure models that
include some elements of metadata (field attributes, relationships, and so
on). Most metadata models take the form of structured (or unstructured)
text, but some more formal notations are available, in particular those based
around XML.
Some automated tools can extract metadata from large databases. Al-
though these are to some extent in their infancy, they can be extremely use-
ful, especially when dealing with legacy systems whose data internals may
not be well understood.
There are some industry standard data models that may be of use in your
metadata analysis, such as the ARTS Standard Relational Data Model for
retail, or the ISO 20022 standard for financial services messaging.
VOLUMETRIC MODELS Volumetric models look at current and predicted data
volumes. These can range from a few simple calculations on a scrap of paper to
sophisticated statistical models to complete online simulations of systems. At
the architectural level, they are usually kept fairly simple because the execution
details of the system aren’t yet known to any degree of accuracy.
AND P ITFALLS
Representation Incompatibilities
At their simplest, data incompatibilities arise because different systems repre-
sent field-level information in different ways, either by using different models
for the information (e.g., polar versus Cartesian coordinates) or simply differ-
ent encoding schemes (e.g., metric or imperial lengths). For example:
One system may use Y and N for Boolean values, while another uses 1
and 0, or hex FF and 00.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 323
One system may use standard ISO abbreviations such as FR or DE for
countries, while another has its own numeric encoding.
One system may record monetary amounts in euros, while another uses
the local currency in which the transaction took place.
One system may record amounts by volume, another by weight.
One system might keep running totals, and another system might just
deal in deltas.
These sorts of problems are usually fairly easy to resolve. Much more
problematic, however, are incompatibilities between business models.
EXAMPLE An architecture is required to integrate a telephone billing
system with another system used to manage prospects, sales, and mar-
keting promotions. A telephone customer may have several phone lines
or may charge calls on a single line to different charge codes; for this
reason, the billing system is based on the concept of a telephone
account. Even worse, some accounts may be held jointly by several cus-
tomers (especially business accounts), and some others (such as public
emergency phone lines) have no real customer at all.
The sales system is concerned solely with customers (and, more
important, prospective customers). However, the system needs to know
about these customers’ existing accounts, as well as other details such
as payment history and usage, in order to avoid trying to sell customers
something they already have.
The business models for these systems are fundamentally incompati-
ble, and a lot of work is going to be needed to develop an architecture
that successfully brings them together.
Incompatible business models can usually be reconciled only by using
what may turn out to be fairly complex processing. In the example, you would
probably have to develop a subsystem or service that was responsible for
maintaining the links between customers and their accounts. This service
would have to be updated (possibly in real time) when customers or accounts
were created, deleted, or updated, or when the links between them were
changed. It would own and manage the information itself and provide that
data on demand to any other architectural element that required it.
Such a service would sit at the core of the architecture, being accessed by
many other architectural elements, with ambitious targets for performance,
scalability, and availability. This service would need to be very carefully
designed, built, and tested.
324
 P A R T III  A V I E W P O I N T C A T A L O G
RISK REDUCTION
 Develop a common, high-level model of the data structure, the key data
attributes, and their domains, and validate it against all parts of the sys-
tem (internal and external).
 Review your model with the business to ensure that it reflects reality.
 Focus on a small number of critically important attributes, rather than
trying to model everything.
 Don’t forget to include external entities in your model (e.g., if you
exchange data with other organizations).
 Consider developing a data abstraction layer on top of data sources to
hide the incompatibilities from other parts of the architecture.
Unavoidable Multiple Updaters
When creating distributed architectures, we all strive to achieve models whereby
each data item is updated in one place and one place only. Unfortunately, in the
real world this ambition cannot always be realized, for a number of reasons: Leg-
acy systems cannot easily be changed, information may be sourced from outside
the organization, or there may be limitations imposed by geography or politics.
As we have seen, multiple creators or updaters can have a significant im-
pact on the architecture, and resolving such problems is not always easy.
From the architectural perspective, you need to be aware of where this can
happen so that you can take suitable measures to mitigate the risks.
RISK REDUCTION
 Ensure that your information ownership model is complete and accurate
and that all data items with multiple updaters are identified.
 Determine with your stakeholders (primarily your users) which of these
multiple updaters are important, and focus on these.
 Understand where inconsistencies through multiple updaters can arise
and locate the crunch points where incompatible data items meet.
 Develop strategies for resolving these, such as always overriding old up-
dates with newer ones, or maintaining two copies of data and resolving
problems manually.
Key-Matching Deficiencies
When you are bringing together information from multiple systems, key-match-
ing problems almost inevitably arise, as we saw earlier. These may not become
apparent until you get into detailed design—by which time it is very expensive to
change the architecture—or, even worse, once the system is running.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 325
RISK REDUCTION
 Make sure that you have identified keys for all entities, and satisfy your-
self that these keys are compatible across the architecture.
 At all points where information from different systems comes together,
ensure that you have the means to map keys from one system to the
other.
 Sample real data and run consistency checks on it.
 Whenever possible, go for common keys and standardized ways of mod-
eling information.
Interface Complexity
If two systems need to transfer information between themselves, one bidirec-
tional interface needs to be built. For three systems, three interfaces are
needed; for four systems, six. In the worst-case situation, if your architecture
comprises n systems, each of which needs to exchange information with
every other, you need to build n(n – 1)/2 interfaces, as shown in Figure 18–8.
Even though it is unlikely that every system in your architecture needs to
exchange information with every other, once you have more than a handful of
systems, the number of interfaces required becomes unmanageable. Change
the interface definition for any one of your n systems, and n – 1 interfaces
need to be redesigned, recoded, tested, and deployed. This represents a signif-
icant burden for developers and often acts as a barrier to change.
RISKREDUCTION
When interface requirements are complex, consider applying an architec-
tural style called the integration hub. In this model, all systems are linked
once via a specialized adapter to one central integration hub. The adapter
performs system-specific translation, and the hub handles message rout-
ing, resilience, and more specialized functions such as publish and sub-
scribe, acknowledgment, and guaranteed delivery. An example is shown
in Figure 18–9.
The advantage of this approach is that if a system changes, often only
the adapter for that system needs to be modified. Furthermore, specialized
code for routing, resilience, and so forth has to be implemented only once,
in the central hub. (Of course, central hubs also have disadvantages in
that they are often a single point of failure, can be a scalability bottleneck,
and ironically can slow down change due to the difficulty of scheduling
and prioritizing changes to such a critical shared component.) A third-
party product is typically used to implement an integration hub. A large
number of such off-the-shelf, highly configurable integration hubs are
available as both commercial and open source products.
Integration hubs and similar architectures (such as a message bus) form
part of the wider topic of Enterprise Application Integration, a full consider-
ation of which is outside the scope of this book. We provide some references
on this subject in the Further Reading section at the end of this chapter.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 327
Overloaded Central Database
Many of the problems described in this chapter can be eliminated by storing
all information in a single central database. This approach is much simpler
and cleaner, since there is no need for key mappings, update reconciliation, or
complex interfaces, and all data is immediately available.
However, a single central database is a single point of failure and will
eventually become a performance bottleneck. For geographically distributed
systems, a central database will give poor latency for remote users, and they
may find that system availability is constrained due to limitations of the glo-
bal network. Managing all data in a single central database can cause the data
model to become overloaded or unworkable and can cause design-time and
runtime contention. For these reasons, care must be taken when designing a
system based on a single central database.
RISK REDUCTION
 Carefully consider the likely growth of your system in terms of data
volumes, numbers of users, and their locations. (We discuss this issue in
Chapter 28 on the Evolution perspective.)
 Consider the deployment (now or in the future) of a reporting database,
separate from the main operational data store, and design your architec-
ture with this possibility in mind.
 Be aware of the need to partition data in the future and design a strategy
for it now (even if it is not yet implemented).
 If you do opt for a single central database, make sure that there are some
scalability options available in case the system is more successful than
expected.
 Look into the use of database clustering technologies and other mechanisms
for improving availability and performance.
Inconsistent Distributed Databases
Conversely, some of the problems described in this chapter can be eliminated by
replicating information between multiple databases in different locations or
even geographical regions. This approach brings data near to where it is
needed, with a consequent reduction in latency and improvement in availability.
However, distributed information architectures are harder to design and
build and often lead to information inconsistency due to the replication delay.
Furthermore, updates are harder to manage in cases where replicate copies
are not read-only. While these problems are not insurmountable, they require
careful design and a solid implementation.
328
 P A R T III  A V I E W P O I N T C A T A L O G
RISK REDUCTION
 Carefully consider the need for a distributed information architecture,
balancing the benefits this brings against the cost in complexity and data
inconsistency.
 If you adopt a distributed model, ensure that you have effective strate-
gies in place for dealing with inconsistency and that these are agreed
upon with your key stakeholders, especially users.
 Ensure that there are effective operational tools and processes in place for
detecting and dealing with problems that can’t be dealt with automatically.
Poor Information Quality
If the actual data is inconsistent, inaccurate, or incomplete, it doesn’t matter
how good your information model is—you will face big problems when your
system goes into operation.
In fact, the real problem is not necessarily poor information quality but
unexpectedly poor information quality. If you know that some information
will be inadequate, you can develop strategies early to deal with it and suc-
cessfully manage the expectations of your stakeholders in this area.
RISK REDUCTION
 Validate your key assumptions about information quality early (e.g., “All
products can be uniquely identified globally by using an immutable com-
mon key”).
 Make sure that you understand what information is important and what
is less important (your stakeholders, primarily users, can tell you this),
then focus on the important information.
 Make use of commercially available information quality tools to analyze
the quality of existing information.
 Identify the places where poor-quality information can appear, and
develop strategies for dealing with it, such as rejecting poor-quality
information, marking it as suspect, or attempting to fix it.
Excessive Information Latency
Excessive latency typically arises from overly complex architectures or archi-
tectures that are not designed to handle the volumes of information they are
presented with. You may also have latency issues that are outside your con-
trol. For example, information may arrive from an external source only once a
week, or updates may need to be applied in batches overnight because of the
limitations of a legacy system.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 329
As with information quality, poor latency becomes an issue only if it is
unexpectedly poor. By identifying expected latency early, you can identify
problem areas and develop strategies to deal with them.
RISK REDUCTION
 When there is distance or complexity between information providers and
information consumers, ensure that you predict, as best you can, what
the information latency will be.
When latency is significant, review this with your stakeholders to deter-
mine whether it is a concern.
Better still, obtain agreement on realistic latency requirements for all
data items up front, and validate your model against these.
Inadequate Volumetrics
A system designed to handle a thousand updates per day is unlikely to cope
well when faced with a million updates per day. Unless you are clear about
the volumes of information the system is expected to handle, you have little
chance of designing an appropriate architecture. (We address the issue of vol-
umetrics in more detail in Chapter 26.)
RISKREDUCTION
Make sure that data volumes are captured, reviewed, and approved by
your stakeholders. You may want to separately capture “business”
volumes (such as numbers of orders) from acquirers and users, and
“technical” volumes (such as numbers of database updates) from
technical stakeholders.
Make sure that volumes are realistic. If the stakeholders convey doubt or
vagueness about this, pursue the issue, and if in doubt, increase them to
allow for the margin of error.
Make sure that your data volumes cover all scenarios—not just the
online day, for example, but also the overnight processing and peak peri-
ods such as the end of the year or holiday processing.
Make sure that there is an effective translation of business volumes into
physical ones. For example, a single business transaction, such as plac-
ing an order, may result in several physical transactions, such as decre-
menting stock levels, posting account records, assigning compensation to
sales staff, and arranging delivery of the ordered item.
Make sure that your volumes take future expansion into account.
Prototype your data stores and the access to them for the expected
volumes you do have.
330
 P A R T III  A V I E W P O I N T C A T A L O G
C HECKLIST
Do you have an appropriate level of detail in your data models (e.g., no
more than about 20–30 entities)?
Does the data model support the processing requirements now and those
likely in the future?
Are keys clearly identified for all important entities?
When an entity is distributed across multiple systems or locations with
different keys, are the mappings between these keys defined? Do you have
processes for maintaining these mappings when data items are created?
Have you taken account of data in one place that is derived from data
managed and owned elsewhere, such as account balances derived from
account activity?
Have you defined strategies for resolving data ownership conflicts,
particularly when there are multiple creators or updaters?
Are latency requirements clearly identified, and are mechanisms in place
to ensure that these are achieved?
Do you have clear strategies for transactional consistency across distrib-
uted data stores, and do these balance this need with the cost in terms of
performance and complexity?
Have you considered which data storage models to use for the various
data stores in your system, taking into account the strengths and weak-
nesses of each?
Do you have mechanisms in place for validating migrated data and deal-
ing appropriately with errors?
Do you have the right sort of data stores (operational data store, report-
ing databases, data warehouses, and data marts) for the expected
volumes and performance requirements?
Have you defined sufficient storage and processing capacity for
archiving? For restoring archived data?
Has a data quality assessment been done? Have you created strategies
for dealing with poor-quality data?
Have you confirmed which entities in your information model should be
obtained from shared enterprise sources, and if so, does your architec-
ture make use of these appropriately?
F URTHER R EADING
The literature on information architecture per se (as opposed to data design
techniques or specific data management technologies) is sparse.
C H A P T E R 18  T H E I N F O R M A T I O N V I E W P O I N T
 331
Fortunately, data modeling, and particularly relational modeling, which un-
derpins much that we do, has a strong theoretical grounding, so there is a
plethora of books on the subject. The classic of the genre, which is still being
updated, is probably Date [DATE03]. Other good general bo oks include
Elmasri and Navathe [ELMA99] and Kroenke [KROE02].
Kim [KIMW99] looks at some of the newer techniques such as object-ori-
ented databases. Redman [REDM97] provides a detailed discussion of the issues
around data quality and how to develop strategies for data quality analysis and
improvement.
Enterprise Application Integration architectures are covered in a large
number of books, such as Linthicum [LINT03] and Ruh et al. [RUHW00].
You can find further information on metadata modeling in ISO Standard
11197-3 [ISO96] and books such as [MARC00]. Information on specific meta-
data models such as the ARTS Standard Relational Data Model or the ISO
20022 standard for financial services messaging can be found on the Web sites
for those organizations.
If you are interested in ideas on how to flexibly evolve a database schema
as part of the software development process, Scott Ambler and Pramod Sad-
alage’s book on database refactoring [AMBL06], which introduces the Evolu-
tionary Database Design technique, will be of interest.
There are many books on data warehousing, from the two pioneers of this
approach, William Inmon (e.g., [INMO05]) and Ralph Kimball (e.g.,
[KIMB02]), and many others.
Alec Sharp and Patrick McDermott’s book [SHAR08] provides a good
description of the subject and the techniques used. A vast number of books
(too numerous to mention here) cover specific relational database products
(e.g., Oracle, SQL Server, DB2, Sybase, MySQL) and tools and technologies for
application development, systems management, and integration.
The best place to obtain information on nonrelational database technolo-
gies, such as the NoSQL movement, is the Internet.
The Data Management Association (DAMA) has much useful informa-
tion, runs conferences and seminars, provides training and certification, and
has chapters worldwide. They can be found at www.dama.org.