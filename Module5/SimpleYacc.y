%{
// Ёти объ€влени€ добавл€ютс€ в класс GPPGParser, представл€ющий собой парсер, генерируемый системой gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token BEGIN END CYCLE INUM RNUM ID ASSIGN SEMICOLON REPEAT UNTIL WHILE DO IF THEN ELSE FOR TO OPENP CLOSEP WRITE VAR COMMA MINUS PLUS MULT DELIM
%%

progr   : block
		;

stlist	: statement 
		| stlist SEMICOLON statement 
		;

statement: assign
		| block  
		| cycle 
		| repeat
		| while
		| if
		| write
		| var
		;
identlist : ID 
		| ID COMMA identlist
		;


ident 	: ID 
		;
	
assign 	: ident ASSIGN expr 
		;

expr	: e1
		;

e1		: e2
		| e1 MINUS e2
		| e1 PLUS e2
		;

e2		: e3
		| e2 MULT e3
		| e2 DELIM e3
		;

e3		: ident | INUM | RNUM | OPENP expr CLOSEP
		;


block	: BEGIN stlist END 
		;

cycle	: CYCLE expr statement 
		;
repeat  : REPEAT stlist UNTIL expr
		;

while   : WHILE expr DO statement
		;

if		: IF expr THEN statement
		| IF expr THEN statement ELSE statement
		;

while   : FOR assign TO expr DO statement
		;

write   : WRITE OPENP expr CLOSEP
		;

var		: VAR identlist
		;
%%
