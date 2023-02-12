$(document).ready(function () {
    var selectedType = localStorage.getItem('selectedType') || 'PrivatePerson';
    if (selectedType !== "Company") {
        $("input[name='ParticipantType'][value='PrivatePerson']").prop('checked', true).change();
        $("#companyFields").hide();
        $("#privatePersonFields").show();
    } else {
        $("input[name='ParticipantType'][value='Company']").prop('checked', true).change();
        $("#privatePersonFields").hide();
        $("#companyFields").show();
    }

    $("input[name='ParticipantType']").change(function () {
        var selectedType = $("input[name='ParticipantType']:checked").val();
        localStorage.setItem('selectedType', selectedType);
        if (selectedType === "PrivatePerson") {
            $("#privatePersonFields").show();
            $("#companyFields").hide();
        } else if (selectedType === "Company") {
            $("#privatePersonFields").hide();
            $("#companyFields").show();
        }
    });

});