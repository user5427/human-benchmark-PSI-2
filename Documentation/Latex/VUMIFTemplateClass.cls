%% VUMIFPSbakalaurinis.cls – „Tik svajotojai-romantikai gali pagerinti nepakeičiamą pasaulį“
%% Created by   „Atviras Kodas Lietuvai“  2015
%
%% Latest version available here: https://github.com/LIKS/bachelor_thesis_template_vu_mif_se
%% Any suggestions or contributions are welcome!  Also see: https://latex.liks.lt!
%
%% License CC BY 4.0 (see more at http://creativecommons.org/licenses/by/4.0/)
% You are free to (a) share - copy and redistribute the material in any medium
% or format (b) adapt - remix, transform, and build upon the material for any
% purpose, even commercially. As long as you give appropriate credit, provide a
% link to the license, and indicate if changes were made.


% Formatting the Bachelor's thesis according to Vilnius University
% Faculty of Mathematics and Informatics requirements
\NeedsTeXFormat{LaTeX2e}
\ProvidesClass{VUMIFTemplateClass}[2024/03/21 work group template class]
\LoadClass[12pt, a4paper]{article}
\RequirePackage{LTPlius}

\ProcessOptions\relax

\RequirePackage{fontspec} % Font selection
% The text font will be Calibri.
\setmainfont[
    Path=fonts/Calibri/,
    Ligatures=TeX,
    Extension=.ttf,
    UprightFont=*-nm,
    BoldFont=*-bd,
    ItalicFont=*-it,
    BoldItalicFont=*-bi,
]{Calibri}

\RequirePackage{setspace} % Line spacing
\RequirePackage[
    left=2cm,
    top=2cm,
    right=2cm,
    bottom=2cm,
    footskip=1.5cm,
]{geometry}  % Page margins
% Distributes text evenly from left to right margins
% \sloppy alternative
\tolerance 1414
\hbadness 1414
\emergencystretch 1.5em
\hfuzz 0.3pt
\widowpenalty=10000
\vfuzz \hfuzz

\RequirePackage[titles]{tocloft} % For setting the alignment of the table of content lines
\renewcommand{\cftsecleader}{\cftdotfill{\cftdotsep}}
\RequirePackage{fancyhdr} % For setting the page number plave

% Sets the position of the page number on the page
\renewcommand{\headrulewidth}{0pt}
\renewcommand{\footrulewidth}{0pt}
\fancyhf{}
\fancyfoot[R]{\thepage} % The number will be at the bottom right of the page
\pagestyle{fancy}

\RequirePackage{caption}

% Variables on the front page
\newcommand{\studyprogramme}[1]{\def\@studyprogramme{#1}}
\newcommand{\worktype}[1]{\def\@worktype{#1}}
\newcommand{\worktitle}[1]{\def\@worktitle{#1}}
\newcommand{\secondworktitle}[1]{\def\@secondworktitle{#1}}
\newcommand{\workauthor}[1]{\def\@workauthor{#1}}
\newcommand{\secondauthor}[1]{\def\@secondauthor{#1}}
\newcommand{\supervisor}[1]{\def\@supervisor{#1}}
\newcommand{\scientificadvisor}[1]{\def\@scientificadvisor{#1}}
\newcommand{\reviewer}[1]{\def\@reviewer{#1}}

% First line of the paragraph is indented 1 cm
\setlength{\parindent}{1cm}

% Line spacing -- 1.5 cm
\setstretch{1.5}

% Algorithm name
\RequirePackage{algorithm}
\makeatletter
\renewcommand{\ALG@name}{Algorithm}
\makeatother

% float package has to be loaded before hyperref
\RequirePackage{float}

% Links in the table of contents and elsewhere are made active
\RequirePackage[%
    hidelinks,
    linktoc=all,
    pdflang={\@ifundefined{@ThesisEng}{lt}{en-US}},
    bookmarksnumbered=true,
]{hyperref}

% Taken form https://tex.stackexchange.com/a/330535
\RequirePackage{bookmark}
\newcommand*{\sectionbookmark}[1][]{%
  \bookmark[%
    level=section,%
    dest=\@currentHref,%
    #1%
  ]%
}

% Title of section not numbered in the table of contents
\newcommand{\sectionnonum}[1]{%
    \section*{#1}%
    \addcontentsline{toc}{section}{#1}%
}

% Title of a chapter not numbered and not included in the contents
\newcommand{\sectionnonumnocontent}[1]{%
    \section*{#1}%
    \sectionbookmark{#1}
}

% Setting the style of the bibliography
\RequirePackage[
    alldates=iso,
    seconds=true,
    % Only the year is shown from the date of publication
    date=year,
    % change syllable splitting when langid is not lithuanian
    autolang=hyphen,
    backend=biber,
    eprint=false,
    sortcites=true,
    sorting=anyt,
    giveninits=true,
    % setting the citation style
    style=numeric, % numerical numbering
    %style=alphabetic, % letter numbering
    abbreviate=false,
    minalphanames=3,
    maxalphanames=3,
    maxnames=9,
    minnames=4,
]{biblatex}

\DefineBibliographyStrings{english}{%
    % List of references.
    in = {in},
    % Terms used in biblatex-iso690 bibliography style.
    % Žr. https://github.com/michal-h21/biblatex-iso690/wiki/Translation-Guideline
    %urlalso = {online access},
    urlfrom = {online access},
    urlseen = {viewed},
    % Žr. https://terminai.vlkk.lt/konsultacijos/1986-on-line-prijungtas-prijungtis
    %online = {online}
}

% The plus sign, which is displayed with >3 authors, is lowered and raised
\renewcommand*{\labelalphaothers}{\textsuperscript{+}}


% Biblatex-iso690 changes

% The DOI field should print the full URL, not just the identifier. Changed from
% https://github.com/michal-h21/biblatex-iso690/blob/7ae03dbdff1f187cfead57c5108a73a6b5021f68/iso.bbx#L377
\DeclareFieldFormat{doi}{\addcolon\space\url{https://doi.org/#1}}

% Author is spelled as First Name Last Name, not Surname, First Name
\DeclareNameAlias{default}{given-family}
% Punctuation between authors is made with commas
\DeclareDelimFormat{multinamedelim}{\addcomma\space}
\DeclareDelimFormat{finalnamedelim}{\addcomma\space}

% We make sure that we do not add [online] to sources that have the url field
% Copied and modified from
% https://github.com/michal-h21/biblatex-iso690/blob/7ae03dbdff1f187cfead57c5108a73a6b5021f68/iso.bbx#L721-L750
\renewbibmacro*{medium-type}{%
  \iffieldundef{howpublished}{}% Don't print anything
    {\ifboolexpr{
        test {\iffieldequalstr{howpublished}{online}}
        and not test {\iftoggle{bbx:url}}
        and not test {\ifentrytype{online}}
      }
        {}% Don't print 'howpublished' field
        {\printfield{howpublished}}}%
}

% Silencing warnings for non-existent files.
\usepackage{silence}
\WarningFilter{biblatex}{File 'american-iso.lbx' not found!}
\WarningFilter{biblatex}{File 'lithuanian-iso.lbx' not found!}


% TODO: pašalinti csquotes.cfg failą 2025 metais, nes csquotes
% paketas pridėjo lietuvišką kabučių stilių 2023-01-24, tad nebereikės
% specialios konfigūracijos, kai nauja versija bus plačiai prieinama
\RequirePackage[autostyle=true]{csquotes}

\RequirePackage{amsthm}

\newtheoremstyle{customStyle}
{}{}{}
{\parindent}
{\bfseries}{. }{}{}

\swapnumbers

\theoremstyle{plain}
\newtheorem{theorem}{Theorem}[subsection]
\newtheorem{lemma}[theorem]{Lemma}
\newtheorem{corollary}[theorem]{Corollary}
\newtheorem{proposition}[theorem]{Proposition}

\theoremstyle{definition}
\newtheorem{definition}[theorem]{Definition}
\newtheorem{remark}[theorem]{Remark}
\newtheorem{example}[theorem]{Example}

\addto\captionenglish{\renewcommand{\proofname}{Proof}}

\captionsetup*[table]{name=, labelsep=space, labelfont=bf, font=it}
\captionsetup*[figure]{name=, labelsep=space, labelfont=bf, font=it}

\renewcommand\thefigure{\arabic{figure} figure.}
\renewcommand\thetable{\arabic{table} table.}
