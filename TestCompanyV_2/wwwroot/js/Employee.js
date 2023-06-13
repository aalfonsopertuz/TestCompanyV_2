$(document).ready(function () {
    module_employee.inicializar();
});

var module_employee = (function () {
    let _nombreModulo;

    let $txtIdentificacion;

    let $btnBuscar;

    let inicializar = () => {
        _nombreModulo = 'module_employee';
        obtenerCampos();
        asociarEventos();
        accionesIniciales();
    };

    let obtenerCampos = () => {
        $txtIdentificacion = $('#txt-identificacion');

        $btnBuscar = $('#btn-buscar');

    }

    let asociarEventos = () => {
        $btnBuscar.on('click', btnBuscar_click);

    }

    let accionesIniciales = () => {
        


    }

    //#region [Funcionalidad]
    function btnBuscar_click() {
        var response = searchPerFilter();

        if (response) {
            getEmployeeById();
        } else {
            getAllEmployees();
        }
    }



    //#endregion

    //#region [Otras Funciones]
    function generateTbl(filter, data) {

        if (filter) {
            var tr = `<tr>
                        <td>`+ data.employee_name + `</td>
                        <td>`+ data.employee_salary + `</td>
                        <td>`+ data.employee_age + `</td>
                        <td>`+ data.employee_salary_per_year + `</td>
                    </tr>`;

            $("#tbl-employee").append(tr);
        } else {
            var filas = data.length;

            for (i = 0; i < filas; i++) {
                var tr = `<tr>
                            <td>`+ data[i].employee_name + `</td>
                            <td>`+ data[i].employee_salary + `</td>
                            <td>`+ data[i].employee_age + `</td>
                            <td>`+ data[i].employee_salary_per_year + `</td>
                        </tr>`;

                $("#tbl-employee").append(tr);
            }
        }

        
    }



    //#endregion

    //#region [Validaciones]
    function searchPerFilter() {
        if ($txtIdentificacion.val().length == 0) {
            return false;
        }
        return true;
    }



    //#endregion

    // #region [Promesas de Carga]

    async function getAllEmployees() {
        $.ajax(
            {
                url: '/api/Employee/GetAllEmployees',
                type: 'GET',
                data: {

                }
            }
        )
            .done(function (data) {
                generateTbl(false, data);
            })
            .fail(function (data) {
                
            })
            .always(function (data) {
                
            });
    }

    async function getEmployeeById() {
        $.ajax(
            {
                url: '/api/Employee/GetEmployeeById/' + parseInt($txtIdentificacion.val()),
                type: 'GET',
                data: {
                    
                }
            }
        )
            .done(function (data) {
                generateTbl(true, data);
            })
            .fail(function (data) {

            })
            .always(function (data) {

            });
    }
    //#endregion



    return {
        inicializar: inicializar
    };


})()