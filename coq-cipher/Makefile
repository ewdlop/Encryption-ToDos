# Coq Makefile for CogCipher

COQMAKEFILE := Makefile.coq

.PHONY: all clean

all: $(COQMAKEFILE)
	$(MAKE) -f $(COQMAKEFILE)

$(COQMAKEFILE): _CoqProject
	coq_makefile -f _CoqProject -o $(COQMAKEFILE)

clean: $(COQMAKEFILE)
	$(MAKE) -f $(COQMAKEFILE) clean
	rm -f $(COQMAKEFILE) $(COQMAKEFILE).conf

install: $(COQMAKEFILE)
	$(MAKE) -f $(COQMAKEFILE) install


