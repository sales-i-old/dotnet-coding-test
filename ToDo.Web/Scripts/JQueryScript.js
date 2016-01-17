$(document).ready(function() {
    $('.todoItem').draggable({
        containment: '#draggableArea',
        revert: 'invalid',
        helper: 'clone',
        cursor: 'move'
    });

    $('#droppableArea').droppable({
        accept: '.todoItem',
        drop: function (event, ui) {
            var task = $(ui.draggable);
            task.fadeOut(200, function () {
                $(this).appendTo('#droppableArea').fadeIn(1000);
            });
        }
    });

    $('#dlTasks').droppable({
        accept: '#droppableArea .todoItem',
        drop: function (event, ui) {
            var task = $(ui.draggable);
            task.fadeOut(200, function () {
                $(this).appendTo('#dlTasks').fadeIn(1000);
            });
        }
    });
});