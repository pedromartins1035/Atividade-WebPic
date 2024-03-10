var viewModel = {
    pessoas: ko.observableArray([]),
    nome: ko.observable(),
    sobrenome: ko.observable(),
    dataNasc: ko.observable(),
    estadoCivil: ko.observable(),
    cpf: ko.observable(),
    rg: ko.observable(),

    salvar: function () {
        var data = {
            nome: viewModel.nome(),
            sobrenome: viewModel.sobrenome(),
            dataNasc: viewModel.dataNasc(),
            estadoCivil: viewModel.estadoCivil(),
            cpf: viewModel.cpf(),
            rg: viewModel.rg()
        };

        $.ajax({
            url: '/Pessoa/Create',
            type: 'POST',
            data: data,
            success: function (response) {
                if (response.success) {
                    alert('Pessoa criada com sucesso!');
                    // Redirecionar para a página de listagem de pessoas
                    window.location.href = '/Pessoa/Index';
                } else {
                    alert(response.errors); // Exibe a mensagem de erro em um alerta
                }
            },
            error: function (xhr, status, error) {
                console.error("Erro ao criar pessoa:", error);
            }
        });

        return false;
    },

    editar: function (codigo) {
        window.location.href = '/Pessoa/Details/' + codigo;
    },

    remover: function (codigo) {
        window.location.href = '/Pessoa/DetailsDelete/' + codigo;
    },


    //editarPessoa: function (codigo) {
    //    $.ajax({
    //        url: '/Pessoa/Details/' + codigo, // Endpoint para buscar os detalhes da pessoa
    //        type: 'GET',
    //        success: function (data) {
    //            // Preenche os observables com os dados da pessoa
    //            viewModel.nome(data.nome);
    //            viewModel.sobrenome(data.sobrenome);
    //            viewModel.dataNasc(data.dataNasc);
    //            viewModel.estadoCivil(data.estadoCivil);
    //            viewModel.cpf(data.cpf);
    //            viewModel.rg(data.rg);

    //            // Redireciona para a página de edição com os dados preenchidos
    //            window.location.href = '/Pessoa/Details/' + codigo;
    //        },
    //        error: function (xhr, status, error) {
    //            console.error("Erro ao carregar dados da pessoa para edição:", error);
    //            // Tratar o erro de acordo com sua necessidade
    //        }
    //    });
    //},

    excluir: function (codigo) {
        if (confirm('Tem certeza que deseja remover essa pessoa?')) {
            $.ajax({
                url: '/Pessoa/Delete/' + codigo,
                type: 'DELETE',
                success: function () {
                    alert('Pessoa deletada com sucesso!');
                    // Recarregar a lista de pessoas após a remoção
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    alert("Erro ao remover pessoa:", error);
                }
            });
        }
    },

    // Função para listar pessoas
    listarPessoas: function () {
        $.ajax({
            url: '/Pessoa/Listar',
            type: 'GET',
            success: function (data) {
                // Formatando a data antes de exibi-la
                data.forEach(function (pessoa) {
                    pessoa.DataNasc = moment(pessoa.DataNasc).format('DD/MM/YYYY');
                });
                viewModel.pessoas(data);
            },
            error: function (xhr, status, error) {
                alert("Erro ao listar pessoas:", error);
            }
        });
    },

    cancelar: function () {
        window.location.href = '/Pessoa/Index';
    }

};

ko.applyBindings(viewModel);

// Chama a função de listar pessoas quando a página for carregada
viewModel.listarPessoas();
