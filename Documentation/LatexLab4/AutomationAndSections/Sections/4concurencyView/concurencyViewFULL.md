Concurrency Viewpoint

Definition:
Describes the concurrency structure of the system, mapping functional elements to concurrency units to identify:

    Parts of the system that execute concurrently.

    Coordination and control mechanisms.

Applicability:

    Information systems with concurrent threads of execution.

    Event-driven, reactive systems (e.g., middleware, e-commerce).

    Not needed for systems where concurrency is managed by underlying frameworks (e.g., databases, application servers).

Key Concerns:

    Task Structure:

        Identifies the system’s process/thread structure.

        Defines workload partitioning across processes/threads.

        Granularity varies:

            Small systems: Focus on thread-level models.

            Large systems: Focus on groups of similar processes.

    Mapping Functional Elements to Tasks:

        Determines isolation vs. cooperation of functional elements.

        Impacts performance, resilience, and flexibility.

    Interprocess Communication (IPC):

        Mechanisms: Remote procedure calls, messaging, shared memory, pipes, queues.

        Challenges: Latency, scalability, throughput.

    State Management:

        Defines runtime states of functional elements, valid transitions, and triggers.

        Focuses on technical state (runtime elements), not persistent business state.

    Synchronization and Integrity:

        Ensures data integrity across concurrent threads.

        Applies to shared variables, critical data stores, etc.

    Supporting Scalability:

        Balances concurrency and synchronization to avoid bottlenecks.

        Avoids over/under-concurrency and naïve synchronization.

Models:

    Process Model: Shows process/thread structure and IPC.

    State Model: Defines runtime states and transitions.

Problems/Pitfalls:

    Deadlock, race conditions, resource contention.

    Excessive complexity, incorrect concurrency modeling.

Stakeholders:

    Developers, testers, communicators, administrators.

Examples:

    Data Warehouse:

        Concurrency handled by underlying DBMS; no explicit architectural control.

    E-Commerce System:

        Message-based processing with concurrent message handling.

        Requires explicit concurrency design to manage shared resources.

Incomplete/Vague Areas:

    No specific examples of state models or process models.

    IPC mechanism trade-offs (e.g., when to use messaging vs. shared memory) not detailed.

    Scalability strategies (e.g., thread pools, load balancing) only hinted at.

Diagram Hints:

    Process model could show process groups and IPC links.

    State model could use state transition diagrams.

Concurrency View
Key Concerns

    Startup and Shutdown

        Managing multiple OS processes requires specific startup/shutdown orders due to intertask dependencies.

        Critical for developers, testers, and administrators to understand.

    Task Failure

        Failure of functional elements in different processes/threads adds complexity (e.g., unreachable tasks).

        Requires system-wide strategies for fault tolerance to prevent cascading failures.

    Reentrancy

        Ability of software elements to function correctly under concurrent thread usage.

        Architecture must define which modules require reentrancy (e.g., email server’s name resolution library).

        Impacts third-party software selection and usage.

Stakeholder Concerns
Stakeholder Class	Concerns
Administrators	Task structure, startup/shutdown, task failure
Communicators	Task structure, startup/shutdown, task failure
Developers	All concerns
Testers	Task structure, functional-to-task mapping, startup/shutdown, task failure, reentrancy
System-Level Concurrency Models
Core Components

    Processes

        OS processes (isolated address spaces) hosting one or more threads.

        Basic unit of concurrency; interprocess communication (IPC) required for interaction.

    Process Groups

        Abstraction for grouping related processes (e.g., DBMS as a single functional unit).

        Hierarchical structuring for large/complex systems.

    Threads

        OS threads (lightweight processes) within a process.

        Typically deferred to subsystem design but may be architecturally significant (e.g., thread pools).

Interprocess Communication (IPC) Mechanisms

    Procedure Call Mechanisms

        Remote procedure calls (RPC) or message-passing.

    Execution Coordination

        Semaphores, mutexes (same-machine coordination).

    Data-Sharing

        Shared memory, distributed tuple spaces (e.g., Linda, GigaSpaces), client/server databases.

    Messaging

        Queuing: FIFO, single-consumer.

        Publish/Subscribe: Topic-based, multi-consumer.

Note: IPC choice impacts performance, scalability, and reliability.
Notation
UML Representation

    Processes/Threads: Stereotyped active components (e.g., <<process>>, <<thread>>).

    Process Groups: Stereotype <<process group>> (e.g., DBMS instance).

    IPC:

        Simple mechanisms: UML associations with directional arrows (e.g., RPC).

        Complex mechanisms: Stereotypes (e.g., <<mutex>>, <<shared memory>>).

Example Models:

    Simple Concurrency Model (Figure 19-1):

        Three processes (client, statistics service, statistics calculator) + DBMS process group.

        Mutex coordinates cross-process activity.

    Thread-Centric Model (Figure 19-2):

        Two processes communicating via socket stream.

        DBMS Process:

            1 Network Listener thread.

            1–40 query-processing threads (IPC queue).

            Up to 10 Disk I/O Manager threads (shared memory with Data Access Engine).

Formal Notations

    Real-time/control systems research languages (e.g., CSP, Petri nets).

    Incomplete Section: Specific notations not detailed in this excerpt.

Areas Requiring Clarification

    UML Stereotypes: Exact tagging conventions for IPC mechanisms (e.g., <<shared memory>>).

    Formal Notations: Missing examples or references to specific languages.

    Thread Design Patterns: Guidance for subsystem designers (hinted but not expanded).

Concurrency View
Process Model Notations

    Formal Notations:

        Examples: LOTOS, Communicating Sequential Processes (CSP), Calculus of Communicating Systems (CCS).

        Characteristics: Textual, mathematical, abstract. Rarely used in industrial information systems due to complexity and training requirements.

    Informal Notations:

        Most common in practice, invented ad-hoc for the problem.

        Must capture:

            Processes and process groups.

            Threads.

            Interprocess communication (IPC) mechanisms.

        Advantages: Simplicity, avoids over-adapting general-purpose notations (e.g., UML).

        Risk: Ambiguity if not clearly defined.

Activities for Process Modeling

    Map Elements to Tasks:

        Decide how functional elements map to processes (1:1, N:M, or shared processes).

        Introduce concurrency only when required (distribution, scalability, isolation).

        Avoid unnecessary overhead from cross-process communication.

    Determine Threading Design:

        Decide thread count per process and allocation strategy.

        Typically handled by subsystem designers, but architects may define:

            Threading patterns.

            Consistency rules for quality properties.

    Define Resource-Sharing Mechanisms:

        Protect shared resources (memory, files, DB objects) from corruption.

        Common approach: Locking protocols.

        Architectural focus: Ensure suitability and avoid system-wide side effects.

    Define IPC Mechanisms:

        Choose communication methods between tasks (e.g., Actor pattern).

        Prefer simple, regular schemes with minimal intertask communication.

        Leverage libraries/frameworks to reduce complexity.

    Assign Thread/Process Priorities:

        Use OS priority levels for critical tasks.

        Risks: Priority inversion, added complexity.

        Recommendation: Keep priority assignments simple and prototype to validate.

    Analyze Deadlocks:

        Techniques: Petri Net Analysis, informal review.

        Goal: Identify potential deadlocks from concurrent resource access.

    Analyze Contention:

        Identify contention points (shared resources under high load).

        Estimate:

            Concurrent task count.

            Resource hold times.

            Wait times and throughput impact.

        Mitigation: Design graceful overload handling (e.g., Circuit Breaker pattern).

State Models

    Purpose: Describe runtime element states and valid transitions.

    Scope: Focus on architecturally significant state machines (visible at system level).

    Key Entities:

        State: Stable, named condition (e.g., waiting for an event).

        Transition: Instantaneous state change triggered by an event.

        Event: Trigger for a transition (e.g., operation invocation, timer expiry).

        Action: Atomic processing executed during a transition.

    Advanced Semantics (optional):

        Guards (conditional transitions).

        Activities (interruptible state-bound processing).

        Hierarchical states.

Notation for State Models

    Primary Notation: UML Statechart.

        Example (Figure 19–3):

            Composite states (e.g., "Running" with substates: "Waiting for Data", "Calibrating Metrics").

            Concurrent substates (e.g., "Calculating Values" and "Calculating Risk" within "Calculating").

            Transitions with events/actions (e.g., reset() on shutdown).

        Key aspects:

            Discard in-progress results if new data arrives during calculation.

            (Other aspect not fully described; incomplete section).

    Alternative Notations:

        Classic state transition diagrams.

        Textual notations (not detailed in source).

Areas Requiring Clarification

    Statechart Example: Second "architecturally significant aspect" is cut off.

    Contention Analysis: Specific formulas/workload scenarios not provided.

    IPC Mechanisms: Tradeoffs between options (e.g., message queues vs. shared memory) not detailed.


Concurrency View (Continued)
State Model Examples & Constraints

    Figure 19–3 (Calculation Engine Statechart):

        Key behaviors:

            Shutdown event: Immediately stops all processing, resets state, and exits (via reset() action).

            New input during calculation: Discards in-progress results (via reset()) and restarts calculation.

            New input during result distribution: Does not interrupt distribution.

        Architectural significance: These behaviors are visible at system level and affect/interact with other elements.

    Figure 19–4 (Architectural Constraint Statechart):

        Distills a constraint:

            Shutdown event must trigger immediate reset in any running state.

            Leaves lower-level state details to subsystem designers.

Alternative State Modeling Notations

    Graphical:

        Options: Simple state transition diagrams, Petri Nets, SDL, David Harel’s Statecharts.

        Recommendation: Prefer UML statecharts due to standardization/familiarity.

    Textual:

        Possible but less human-readable (e.g., Finite State Processes language).

        Use cases: Machine processing of models.

State Modeling Activities

    Define Notation:

        Select and tailor notation (e.g., UML statechart subset) before modeling.

    Identify States:

        Focus on system-visible states (external impact).

        Avoid confusing activities with states (validate by testing as activity diagrams).

    Design State Transitions:

        Define triggers (events) and atomic actions per transition.

        Ensure events/actions align with element operations/state.

Problems & Pitfalls

    Modeling the Wrong Concurrency:

        Risk: Over-designing thread-level details (non-architectural).

        Mitigation:

            Focus on system-wide concurrency structure, functional-to-process mapping, and system-level state.

            Delegate detailed threading to subsystem designers.

            Specify common patterns (e.g., thread pools) where needed.

    Modeling Concurrency Incorrectly:

        Common mistakes:

            Modeling activities as states (creating accidental activity diagrams).

            Non-terminal states with no exit path (missing events).

            Untraversable transitions (invalid event/condition combos).

            Excessive trivial transitions (trigger-only or action-only).

        Mitigation:

            Master notation/tool semantics upfront.

            Validate via "play computer" walkthroughs or tool animation.

            Watch for naming anomalies (e.g., verb-named states).

    Excessive Complexity:

        Risk: Overly complex concurrency increases delivery/support effort.

        Mitigation:

            Justify all concurrency via stakeholder benefits.

            Use simplest possible state modeling subset.

    Resource Contention:

        Symptoms: Long waits, "hot spots".

        Mitigation:

            Early analysis via usage scenarios (predict high-concurrency areas).

            Techniques:

                Fine-grained locking.

                Optimistic locking.

                Immutable shared resources.

                Reduced concurrency at bottlenecks.

                Approximate/loosely consistent data.

    Deadlock:

        Cause: Circular wait (Task A → Resource 1 → Task B → Resource 2 → Task A).

        Mitigation:

            Fixed lock acquisition order.

            Task isolation.

            Minimize lock scope/duration.

            Leverage deadlock-detection tools (e.g., DBMS).

    Race Conditions:

        Cause: Unplanned concurrent access (e.g., multiple threads incrementing a counter).

        Impact: Data corruption, unpredictable behavior.

        Incomplete Section: Mitigation strategies cut off in source.

Risk Reduction Summary

    General:

        Involve lead developers early for detailed concurrency design.

        Prototype priority assignments and locking schemes.

    State Models:

        Validate via animation or manual walkthroughs.

    Contention/Deadlock:

        Design for graceful degradation (e.g., Circuit Breaker pattern).

Areas Requiring Clarification

    Race Conditions: Missing mitigation strategies (original text cuts off).

    Figure References: No visual details for Figures 19–3/19–4 (only descriptions).

    Tool-Specific Guidance: No examples of UML tool implementations for statecharts.

Concurrency Viewpoint
Risk Reduction
Ensure that there are no unprotected, shared system-level resources that can cause race conditions.
Use immutable data structures where possible to avoid the possibility of race conditions.
Automatically introduce protection mechanisms for all potentially shared resources.
Ensure that the definition of each element interface clearly states whether or not the interface is reentrant.
Checklist
System-Level Concurrency Model

Is there a clear system-level concurrency model?
Are your models at the right level of abstraction? Have you focused on the architecturally significant aspects?
Concurrency Design

Can you simplify your concurrency design?
Do all interested parties understand the overall concurrency strategy?
Mapping Functional Elements

Have you mapped all functional elements to a process (and thread if necessary)?
Do you have a state model for at least one functional element in each process and thread? If not, are you sure the processes and threads will interact safely?
Interprocess Communication

Have you defined a suitable set of interprocess communication mechanisms to support the interelement interactions defined in the Functional View?
Shared Resource Protection

Are all shared resources protected from corruption?
Intertask Communication

Have you minimized the intertask communication and synchronization required?
Resource Hot Spots

Do you have any resource hot spots in your system? If so:
Have you estimated the likely throughput, and is it high enough?
Do you know how you would reduce contention at these points if forced to later?
Deadlock Prevention

Can the system possibly deadlock? If so:
Do you have a strategy for recognizing and dealing with this when it occurs?
Further Reading
Concurrency Overview:
Magee and Kramer [MAGE06]: A Java-specific overview of concurrency, including formal modeling and analysis. Introduces the Finite State Processes language.
State Modeling:
Cook and Daniels [COOK94]: Discusses statecharts for modeling object state. Available online at www.syntropy.co.uk/syntropy.
Rumbaugh et al. [RUMB99]: UML-specific advice on state modeling, organized as a reference.
Visual Formalisms:
Girauld and Valk [GIRA02]: Explains applying Petri Nets to concurrency analysis.
SDL Forum Society Web site [SDL02]: Resource for SDL (Specification and Description Language).
Concurrency Theories:
Roscoe [ROSC97]: Reference on CSP (Communicating Sequential Processes).
Milner [MILN89]: Definitive book on CCS (Calculus of Communicating Systems).
Harel [HARE87]: Original reference for statecharts.
Practical Concurrency:
Michael Nygard [NYGA07]: Advice on safely introducing concurrency into systems.
Schmidt et al. [SCHM00]: Design patterns for creating concurrent systems.
Breshears [BRES0]: Programming-level introduction to concurrency practice.