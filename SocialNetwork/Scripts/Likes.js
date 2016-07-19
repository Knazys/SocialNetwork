
$(document).ready(function ()
{
    $('.post-like').on('click', sendLike);
});



var sendLike = function ()
{
    var postLike = $(this);
    var postId = postLike.data('post-id');
    var url = postLike.data('url');

    $.ajax({
        url: url,
        method: 'POST',
        data: { id: postId },
        dataType: 'json',
        success: function(response)
        {
            console.log(response);
            if (response.status == true)
            {
                $('.likes-count').html(response.count);
            }
        },
        error: function () {
            console.log('При обработке данных произошла ошибка');
        }
    });

};