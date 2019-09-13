Cart = {
    _properties: {
        addToCartLink: "",
        decrementFromCartLink: "",
        removeFromCartLink: ""
    },

    init: function (properties) {
        $.extend(Cart._properties, properties);
        $(".CallAddToCart").click(Cart.addToCart);
        $(".CallDecrementFromCart").click(Cart.decrementFromCartLink);
        $(".CallRemoveFromCart").click(Cart.removeFromCart);
    },

    /**
     * @param {Event} event The date
     */
    addToCart: function (event) {
        var button = $(this);
        event.preventDefault();
        var id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () { alert("Ok"); Cart.addCountProductId(id); })
            .fail(function () { alert("Not Ok"); });
    },

    decrementFromCartLink: function (event) {
        var button = $(this);
        event.preventDefault();
        var id = button.data("id");

        $.get(Cart._properties.decrementFromCartLink + "/" + id)
            .done(function () { alert("Ok"); Cart.subCountProductId(id); })
            .fail(function () { alert("Not Ok"); });
    },

    removeFromCart: function (event) {
        var button = $(this);
        event.preventDefault();
        var id = button.data("id");

        $.get(Cart._properties.removeFromCartLink + "/" + id)
            .done(function () {
                button.closest("tr").remove();
            })
            .fail(function () { alert("Not Ok"); });
    },

    addCountProductId: function (id) {
        var span_id = "span#prod_count_" + id;
        var current_val = parseInt($(span_id).text());
        $(span_id).html("&emsp;" + (current_val + 1) + "&emsp;");
    },

    subCountProductId: function (id) {
        var span_id = "span#prod_count_" + id;
        var current_val = parseInt($(span_id).text());
        if (current_val > 1)
            $(span_id).html("&emsp;" + (current_val - 1) + "&emsp;");
        else button.closest("tr").remove();
    }
};