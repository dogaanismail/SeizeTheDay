(function($) {
	$.fn.extend({
		faceMocion: function(opciones) {
			var faceMocion = this;
			var NombreSelector = "Selector";
			var DescripcionFace = "--";
			defaults = {
				emociones: [{
					"emocion": "amo",
					"TextoEmocion": "Lo amo"
				}, {
					"emocion": "molesto",
					"TextoEmocion": "Me molesta"
				}, {
					"emocion": "asusta",
					"TextoEmocion": "Me asusta"
				}, {
					"emocion": "divierte",
					"TextoEmocion": "Me divierte"
				}, {
					"emocion": "gusta",
					"TextoEmocion": "Me gusta"
				}, {
					"emocion": "triste",
					"TextoEmocion": "Me entristece"
				}, {
					"emocion": "asombro",
					"TextoEmocion": "Me asombra"
				}, {
					"emocion": "alegre",
					"TextoEmocion": "Me alegra"
				}],
				callback: function() {
					//callbackhere
				}
			};
			var opciones = $.extend({}, defaults, opciones);

            var isMobile = {
                Android: function () {
                    return navigator.userAgent.match(/Android/i);
                },
                BlackBerry: function () {
                    return navigator.userAgent.match(/BlackBerry/i);
                },
                iOS: function () {
                    return navigator.userAgent.match(/iPhone|iPad|iPod/i);
                },
                Opera: function () {
                    return navigator.userAgent.match(/Opera Mini/i);
                },
                Windows: function () {
                    return navigator.userAgent.match(/IEMobile/i);
                },
                any: function () {
                    return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
                }
            };


			$(faceMocion).each(function(index) {
				var UnicoID = Date.now();
                $(this).attr("class", $(faceMocion).attr("class") + " " + UnicoID);
                  
				var EstadoInicial = "alegre";
				if ($(this).val() !== "") {
					EstadoInicial = $(this).val();
				} else {
					$(this).val('alegre');
				}
				DescripcionFace = EstadoInicial;
				ElementoIniciar = '';
				ElementoIniciar = ElementoIniciar + '<div dato-descripcion="' + DescripcionFace + '" ';
				ElementoIniciar = ElementoIniciar + 'id-referencia="' + UnicoID;
				ElementoIniciar = ElementoIniciar + '"  class="' + NombreSelector;
				ElementoIniciar = ElementoIniciar + ' selectorFace ' + EstadoInicial + '"></div>';
				$(this).before(ElementoIniciar);
			});


			$(document).ready(function() {
				BarraEmociones = '<div class="faceMocion">';
				$.each(opciones.emociones, function(index, emo) {
					BarraEmociones = BarraEmociones + '<div dato-descripcion="' + emo.TextoEmocion;
					BarraEmociones = BarraEmociones + '" class="' + emo.emocion + '"></div>';
				});
				BarraEmociones = BarraEmociones + '</div>';
				$(document.body).append(BarraEmociones);
                $('.faceMocion div').hover(function () {
                    var title = $(this).attr('dato-descripcion');
                    $(this).data('tipText', title).removeAttr('dato-descripcion');
                    $('<p class="MensajeTexto"></p>').text(title).appendTo('body').fadeIn('slow');
                }, function () {
                    $(this).attr('dato-descripcion', $(this).data('tipText'));
                    $('.MensajeTexto').remove();
                });
            });

            var isMobile2 = navigator.userAgent.match(/(iPad)|(iPhone)|(iPod)|(Android)|(webOS)/i);
            if (isMobile2 !== null)
            {
                $('.' + NombreSelector).click( function (e) {
                    SelectorEmocion = $(this);
                    var RatonX = e.pageX - 180;
                        var RatonY = e.pageY - 40;
                        $(".faceMocion").css({
                            top: RatonY,
                            left: RatonX
                        });
                        $(".faceMocion").show();
                });
            }
            else
            {
                $('.' + NombreSelector).mouseenter(function (e) {
                    SelectorEmocion = $(this);
                        var RatonX = e.pageX - 200;
                        var RatonY = e.pageY - 50;
                        $(".faceMocion").css({
                            top: RatonY,
                            left: RatonX
                        });
                        $(".faceMocion").show();                  
                });
            }		            
			$(document).on("click", ".faceMocion div", function() {
				SelectorEmocion.attr("class", NombreSelector + " selectorFace  " + $(this).attr('class'));
				ElInputSeleccionado = SelectorEmocion.attr("id-referencia");
				$("." + ElInputSeleccionado).val($(this).attr('class'));
			});
			$(document).mouseup(function(e) {
				$(".faceMocion").hide();
			});
			$(faceMocion).hide();
        },


        faceMocionComment: function (options) {
            var faceMocionComment = this;
            var SelectorComment = "SelectorComment";
            var DescriptionFace = "--";
            defaults = {
                emocions: [{
                    "emocion": "amo",
                    "TextoEmocion": "Lo amo"
                }, {
                    "emocion": "molesto",
                    "TextoEmocion": "Me molesta"
                }, {
                    "emocion": "asusta",
                    "TextoEmocion": "Me asusta"
                }, {
                    "emocion": "divierte",
                    "TextoEmocion": "Me divierte"
                }, {
                    "emocion": "gusta",
                    "TextoEmocion": "Me gusta"
                }, {
                    "emocion": "triste",
                    "TextoEmocion": "Me entristece"
                }, {
                    "emocion": "asombro",
                    "TextoEmocion": "Me asombra"
                }, {
                    "emocion": "alegre",
                    "TextoEmocion": "Me alegra"
                }],
                callback: function () {
                    //callbackhere
                }
            };
            var options = $.extend({}, defaults, options);

            var isMobile = {
                Android: function () {
                    return navigator.userAgent.match(/Android/i);
                },
                BlackBerry: function () {
                    return navigator.userAgent.match(/BlackBerry/i);
                },
                iOS: function () {
                    return navigator.userAgent.match(/iPhone|iPad|iPod/i);
                },
                Opera: function () {
                    return navigator.userAgent.match(/Opera Mini/i);
                },
                Windows: function () {
                    return navigator.userAgent.match(/IEMobile/i);
                },
                any: function () {
                    return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
                }
            };

            $(faceMocionComment).each(function (index) {
                var UniqueID = Date.now();
                $(this).attr("class", $(faceMocionComment).attr("class") + " " + UniqueID);                     
               var InitialValue = "alegre";
                if ($(this).val() !== "") {
                    InitialValue = $(this).val();
                } else {
                    $(this).val('alegre');
                }
                DescriptionFace = InitialValue;
                ElementInitial = '';
                ElementInitial = ElementInitial + '<div dato-descripcion="' + DescriptionFace + '" ';
                ElementInitial = ElementInitial + 'id-referencia="' + UniqueID;
                ElementInitial = ElementInitial + '"  class="' + SelectorComment;
                ElementInitial = ElementInitial + ' selectorFace ' + InitialValue + '"></div>';
                $(this).before(ElementInitial);
            });

            $(document).on("mouseenter", ".inputComment", function (e) {
                var target = e.target;
                console.log(target.getAttribute('id'));
            });


            $(document).ready(function () {
                SelectEmojies = '<div class="faceMocionComment">';
                $.each(options.emocions, function (index, emo) {
                    SelectEmojies = SelectEmojies + '<div dato-descripcion="' + emo.TextoEmocion;
                    SelectEmojies = SelectEmojies + '" class="' + emo.emocion + '"></div>';
                });
                SelectEmojies = SelectEmojies + '</div>';
                $(document.body).append(SelectEmojies);
                $('.faceMocionComment div').hover(function () {
                    var title = $(this).attr('dato-descripcion');
                    $(this).data('tipText', title).removeAttr('dato-descripcion');
                    $('<p class="MensajeTexto"></p>').text(title).appendTo('body').fadeIn('slow');
                }, function () {
                    $(this).attr('dato-descripcion', $(this).data('tipText'));
                    $('.MensajeTexto').remove();
                });
            });

            var isMobile2 = navigator.userAgent.match(/(iPad)|(iPhone)|(iPod)|(Android)|(webOS)/i);
            if (isMobile2 !== null) {
                $('.' + SelectorComment).click(function (e) {
                    SelectorEmoji = $(this);
                    var RatonX = e.pageX - 180;
                    var RatonY = e.pageY - 40;
                    $(".faceMocionComment").css({
                        top: RatonY,
                        left: RatonX
                    });
                    $(".faceMocionComment").show();
                });
            }
            else {
                $('.' + SelectorComment).mouseenter(function (e) {
                    SelectorEmoji = $(this);
                    var RatonX = e.pageX - 200;
                    var RatonY = e.pageY - 50;
                    $(".faceMocionComment").css({
                        top: RatonY,
                        left: RatonX
                    });
                    $(".faceMocionComment").show();
                });
            }

            $(document).on("click", ".faceMocionComment div", function (index) {
                SelectorEmoji.attr("class", SelectorComment + " selectorFace  " + $(this).attr('class'));
                InputSelection = SelectorEmoji.attr("id-referencia");
                $("." + InputSelection).val($(this).attr('class'));                  
            });
            $(document).mouseup(function (e) {
                $(".faceMocionComment").hide();
            });
            $(faceMocionComment).hide();
        }


	});
})(jQuery);