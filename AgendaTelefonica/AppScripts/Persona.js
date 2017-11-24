new Vue({
    el: "#app",
    data: {
        Telefono: {
            TipoTelefono: "",
            Numero: "",
            TipoTelefono2:"",
            Numero2: "",
            Telefonos: [],
            Id :0
        },
        
        Persona: {
            Cedula: "",
            Nombre: "",
            Apellidos: "",
            Genero: "",
            FechaNacimiento: "",
            Telefonos: [],
            Correo: {
                Email: ""
            },
            Direccion: {
                Pais: "",
                DireccionP:""
            }
        },
        Personas: [],
        Telefonos:[],
        valorBusqueda: '',
        telefonosPersonaSeleccionada: [],
        urlGuardarPersona: "http://localhost:49532/api/data/guardarpersona",
        urlGuardarTelefono: "http://localhost:49532/api/data/agregartelefono",
        urlBuscarPersonas: "http://localhost:49532/api/data/buscarpersonas",
        urlBuscarTelefonos: "http://localhost:49532/api/data/buscartelefonos",
        urlEditarPersona: "http://localhost:49532/api/data/editarpersona",
        urlEliminarPersona: "http://localhost:49532/api/data/eliminarpersona",
        urlEliminarTelefono: "http://localhost:49532/api/data/eliminartelefono",
        urlEditarTelefonos: "http://localhost:49532/api/data/editartelefono",
        urlGenerarReporte: "http://localhost:49532/api/data/generarreporte",
    },
    methods: {
        //lIMPIAR FORMULARIOS
        LimpiarFormulario: function () {

            this.Persona.Cedula = "";
            this.Persona.Nombre = "";
            this.Persona.Apellidos = "";
            this.Persona.FechaNacimiento = "";
            this.Persona.Genero = "";
            this.Telefono.Numero = "";
            this.Telefono.TipoTelefono = "";
            this.Telefono.Numero = "";
            this.Telefono.TipoTelefono = "";
            this.Telefono.Numero2 = "";
            this.Telefono.TipoTelefono2 = "";
            this.Persona.Correo.Email = "";
            this.Persona.Direccion.DireccionP = "";
            this.Persona.Direccion.Pais = "";

        },
        limpiarFormularioAgregarTelefono: function () {
            this.Telefono.Numero2 = "";
        },
        //FUNCIONES PARA AGREGAR
        gudardarPersona: function () {
            this.agregarTelefono();
            var vm = this;
            axios.post(this.urlGuardarPersona, this.Persona)
                .then(function (result) {
                    var funciono = result.data;
                    if (funciono) {
                        vm.buscarPersonas();
                        vm.buscarTelefonos();
                        alert("Registro alamacenado");
                        document.getElementById("cedula").focus();
                        vm.LimpiarFormulario();
                        
                    }
                    else {
                        alert("No se guardó");
                    }
                    
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        guardarTelefono: function () {
            var vm = this;
            axios.post(this.urlGuardarTelefono, this.Telefono)

                .then(function (result) {
                    var guardadoExitoso = result.data;
                    if (guardadoExitoso) {
                        vm.buscarTelefonos();
                        alert("Registro almacenado.");
                        vm.Telefono.Numero2 = "";
                        vm.Telefono.TipoTelefono2 = "";
                        return;
                    }
                    alert("Registro almacenado.");
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        //FUNCIONES PARA EDITAR
        EditarPersona: function (modal) {
            var vm = this;
            axios.post(this.urlEditarPersona, this.Persona)
                .then(function (result) {
                    var editadoExitoso = result.data;
                    if (editadoExitoso) {
                        vm.buscarPersonas();
                        alert("Registro actualizado.");
                        vm.OcultarModal(modal);
                        return;
                    }
                    else {
                        alert("Debe realizar algun cambio en la información actual.");
                        return;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        EditarTelefono: function (modal) {
            var vm = this;
            axios.post(this.urlEditarTelefonos, this.Telefono)
                .then(function (result) {
                    var editadoExitoso = result.data;
                    if (editadoExitoso) {
                        alert("Registro actualizado");
                        vm.buscarTelefonos();
                        vm.OcultarModal(modal);
                    }
                    else {
                        alert("Debe realizar algun cambio en la información actual.");
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        agregarTelefono: function () {

            var vm = this;
            this.Persona.Telefonos.push({
                TipoTelefono: this.Telefono.TipoTelefono,
                Numero: this.Telefono.Numero
            });
            if (vm.Telefono.Numero2.length > 0 && vm.Telefono.TipoTelefono2.length > 0) {
                this.Persona.Telefonos.push({
                    TipoTelefono: this.Telefono.TipoTelefono2,
                    Numero: this.Telefono.Numero2
                });
            }

        },
        //FUNCIONES PARA ELIMINAR
        EliminarPersona: function (modal) {
            var vm = this;
            axios.post(this.urlEliminarPersona, this.Persona)
                .then(function (result) {
                    var elimonadoExitoso = result.data;
                    if (elimonadoExitoso) {
                        vm.buscarPersonas();
                        alert("Registro eliminado");
                        vm.OcultarModal(modal);
                        return;
                    }
                    alert("No se puedo eliminar.")
                })
                .catch(function (error) {
                    cosole.log(error);
                });
        },
        EliminarTelefono: function (modal) {
            var vm = this;
            axios.post(this.urlEliminarTelefono, this.Telefono)
                .then(function (result) {
                    var eliminadoExitoso = result.data;
                    if (eliminadoExitoso) {
                        alert("Registro eliminado.");
                        vm.buscarTelefonos();
                        vm.OcultarModal(modal);
                        return;
                    }
                    else {
                        alert("No se puedo eliminar el registro")
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        //FUNCIONES PARA BUSCAR
        buscarPersonas: function () {
            var vm = this;
            axios.get(this.urlBuscarPersonas)
                .then(function (result) {
                    vm.Personas = result.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        buscarTelefonos: function () {
            var vm = this;
            axios.get(this.urlBuscarTelefonos)
                .then(function (result) {
                    vm.Telefonos = result.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        //FUNCIONES PARA ABRIR Y CERRAR MODAL Y OPERACINES QUE DEPENDEN DE ESTE.
        MostrarModalT: function (modal, telefono) {
            $(modal).modal("show"); // abrir
            var vm = this;

            this.Telefono.Numero = telefono.Numero;
            this.Telefono.TipoTelefono = telefono.TipoTelefono;
            this.Telefono.Id = telefono.Id;
        },
        MostrarModalP: function (modal, persona) {
            $(modal).modal("show"); // abrir
            this.Persona.Cedula = persona.Cedula;
            this.Persona.Nombre = persona.Nombre;
            this.Persona.Apellidos = persona.Apellidos;
            this.Persona.FechaNacimiento = persona.FechaNacimiento;
            this.Persona.Genero = persona.Genero;
            this.Persona.Correo.Email = persona.Correo;
            this.Persona.Direccion.Pais = persona.Pais;
            this.Persona.Direccion.DireccionP = persona.DireccionP;
            //this.filtrarNumeroPersonas(persona.Cedula);//filtara los numero de telefonos
            ///////////////////////////////////////////////////////
        },
        MostrarModalA: function (modal, telefono) {
            $(modal).modal("show");
            this.Telefono.Id = telefono.Id;
        },
        MostrarModal: function (modal) {
            $(modal).modal("show");
        },
        OcultarModal: function (modal) {
            $(modal).modal("hide");
        },

        //FUNCION PARA REPORTES
        //GenerarReporte: function (persona) {
        //    //Inicializar la persona seleccionada
        //    this.Persona.Cedula = persona.Cedula;
        //    this.Persona.Nombre = persona.Nombre;
        //    this.Persona.Apellidos = persona.Apellidos;
        //    this.Persona.FechaNacimiento = persona.FechaNacimiento;
        //    this.Persona.Genero = persona.Genero;
        //    this.Persona.Correo.Email = persona.Correo;
        //    this.Persona.Direccion.Pais = persona.Pais;
        //    this.Persona.Direccion.DireccionP = persona.DireccionP;
        //    var vm = this;
        //    axios.post(this.urlGenerarReporte, this.Persona)
        //        .then(function (result) {
        //            var reporteExitoso = result.data;
        //            if (!reporteExitoso) {
        //                alert("Error al genera el reporte");
        //            }
        //        })
        //        .catch(function (error) {
        //            console.log(error);
        //        });
        //}
    },
    computed: {
        //VALIDAR FORMULARIO
        validarFormularPersona: function () {
            var vm = this;
            if (vm.Persona.Cedula.length >= 11 && vm.Persona.Nombre.length >2 && vm.Persona.Apellidos.length > 3
                && vm.Persona.Genero.length > 0 && vm.Persona.FechaNacimiento.length >=9 && vm.Telefono.Numero.length >=10
                && vm.Telefono.TipoTelefono.length > 0  && vm.Persona.Direccion.DireccionP.length > 3
                && vm.Persona.Direccion.Pais.length > 3) 
            {
                return true;
                
            }
            else
            {
                return false;
            }          
        },
        validarFormularioEditarPersona: function () {

            return this.Persona.Nombre.length > 2 && this.Persona.Apellidos.length > 3 && this.Persona.Direccion.DireccionP.length > 3 &&
                this.Persona.Direccion.Pais.length > 3;
        },
        validarFormularTelefono: function () {
            return (this.Telefono.Numero2.length >=10 && this.Telefono.TipoTelefono2.length > 0 )
                || (this.Telefono.Numero.length >= 10 && this.Telefono.TipoTelefono.length > 0);
        },
        validarRegistrosPersonas: function () {
            return this.Personas.length > 0;
        },
        validarRegistrosTelefonos: function () {
            return this.Persona.Telefonos.length > 0;
        },
        filtroPorGenero: function () {
            var vm = this;
            this.valorBusqueda = this.valorBusqueda.toUpperCase();
            if (this.valorBusqueda === '') return this.Personas;
            var personas = this.Personas.filter(function (persona) {
                if (persona.Cedula === vm.valorBusqueda) {
                    return persona.Cedula;
                }
                else if (persona.Nombre === vm.valorBusqueda) {
                    return persona.Nombre;
                }
                else if (persona.Apellidos === vm.valorBusqueda) {
                    return persona.Apellidos;
                }
                
            });
            return personas;
        },
        filtroNumeroTelefono: function () {
            var vm = this;
            if (vm.Persona.Cedula === '') return this.Telefonos;
            var telefonos = this.Telefonos.filter(function (tel) {
                if (tel.Cedula === vm.Persona.Cedula) {
                    return tel;
                }
            });
            this.Persona.Telefonos = telefonos;
            return telefonos;
        },
    },
    beforeMount: function () {
        this.buscarPersonas();
        this.buscarTelefonos();
    }
});