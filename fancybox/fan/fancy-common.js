

    var defaults = {
      // Close existing modals
      // Set this to false if you do not need to stack multiple instances
      closeExisting: false,
    
      // Enable infinite gallery navigation
      loop: false,
    
      // Horizontal space between slides
      gutter: 50,
    
      // Enable keyboard navigation
      keyboard: true,
    
      // Should allow caption to overlap the content
      preventCaptionOverlap: true,
    
      // Should display navigation arrows at the screen edges
      arrows: true,
    
      // Should display counter at the top left corner
      infobar: true,
    
      // Should display close button (using `btnTpl.smallBtn` template) over the content
      // Can be true, false, "auto"
      // If "auto" - will be automatically enabled for "html", "inline" or "ajax" items
      smallBtn: "auto",
    
      // Should display toolbar (buttons at the top)
      // Can be true, false, "auto"
      // If "auto" - will be automatically hidden if "smallBtn" is enabled
      toolbar: "auto",
    
      // What buttons should appear in the top right corner.
      // Buttons will be created using templates from `btnTpl` option
      // and they will be placed into toolbar (class="fancybox-toolbar"` element)
      buttons: [
        "zoom",
        //"share",
        "slideShow",
        //"fullScreen",
        //"download",
        "thumbs",
        "close"
      ],
    
      // Detect "idle" time in seconds
      idleTime: 3,
    
      // Disable right-click and use simple image protection for images
      protect: false,
    
      // Shortcut to make content "modal" - disable keyboard navigtion, hide buttons, etc
      modal: false,
    
      image: {
        // Wait for images to load before displaying
        //   true  - wait for image to load and then display;
        //   false - display thumbnail and load the full-sized image over top,
        //           requires predefined image dimensions (`data-width` and `data-height` attributes)
        preload: false
      },
    
      ajax: {
        // Object containing settings for ajax request
        settings: {
          // This helps to indicate that request comes from the modal
          // Feel free to change naming
          data: {
            fancybox: true
          }
        }
      },
    
      iframe: {
        // Iframe template
        tpl:
          '<iframe id="fancybox-frame{rnd}" name="fancybox-frame{rnd}" class="fancybox-iframe" allowfullscreen allow="autoplay; fullscreen" src=""></iframe>',
    
        // Preload iframe before displaying it
        // This allows to calculate iframe content width and height
        // (note: Due to "Same Origin Policy", you can't get cross domain data).
        preload: true,
    
        // Custom CSS styling for iframe wrapping element
        // You can use this to set custom iframe dimensions
        css: {},
    
        // Iframe tag attributes
        attr: {
          scrolling: "auto"
        }
      },
    
      // For HTML5 video only
      video: {
        tpl:
          '<video class="fancybox-video" controls controlsList="nodownload" poster="{{poster}}">' +
          '<source src="{{src}}" type="{{format}}" />' +
          'Sorry, your browser doesn\'t support embedded videos, <a href="{{src}}">download</a> and watch with your favorite video player!' +
          "</video>",
        format: "", // custom video format
        autoStart: true
      },
    
      // Default content type if cannot be detected automatically
      defaultType: "image",
    
      // Open/close animation type
      // Possible values:
      //   false            - disable
      //   "zoom"           - zoom images from/to thumbnail
      //   "fade"
      //   "zoom-in-out"
      //
      animationEffect: "zoom",
    
      // Duration in ms for open/close animation
      animationDuration: 366,
    
      // Should image change opacity while zooming
      // If opacity is "auto", then opacity will be changed if image and thumbnail have different aspect ratios
      zoomOpacity: "auto",
    
      // Transition effect between slides
      //
      // Possible values:
      //   false            - disable
      //   "fade'
      //   "slide'
      //   "circular'
      //   "tube'
      //   "zoom-in-out'
      //   "rotate'
      //
      transitionEffect: "fade",
    
      // Duration in ms for transition animation
      transitionDuration: 366,
    
      // Custom CSS class for slide element
      slideClass: "",
    
      // Custom CSS class for layout
      baseClass: "",
    
      // Base template for layout
      baseTpl:
        '<div class="fancybox-container" role="dialog" tabindex="-1">' +
        '<div class="fancybox-bg"></div>' +
        '<div class="fancybox-inner">' +
        '<div class="fancybox-infobar"><span data-fancybox-index></span>&nbsp;/&nbsp;<span data-fancybox-count></span></div>' +
        '<div class="fancybox-toolbar">{{buttons}}</div>' +
        '<div class="fancybox-navigation">{{arrows}}</div>' +
        '<div class="fancybox-stage"></div>' +
        '<div class="fancybox-caption"></div>' +
        "</div>" +
        "</div>",
    
      // Loading indicator template
      spinnerTpl: '<div class="fancybox-loading"></div>',
    
      // Error message template
      errorTpl: '<div class="fancybox-error"><p>{{ERROR}}</p></div>',
    
      btnTpl: {
        download:
          '<a download data-fancybox-download class="fancybox-button fancybox-button--download" title="{{DOWNLOAD}}" href="javascript:;">' +
          '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M18.62 17.09V19H5.38v-1.91zm-2.97-6.96L17 11.45l-5 4.87-5-4.87 1.36-1.32 2.68 2.64V5h1.92v7.77z"/></svg>' +
          "</a>",
    
        zoom:
          '<button data-fancybox-zoom class="fancybox-button fancybox-button--zoom" title="{{ZOOM}}">' +
          '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M18.7 17.3l-3-3a5.9 5.9 0 0 0-.6-7.6 5.9 5.9 0 0 0-8.4 0 5.9 5.9 0 0 0 0 8.4 5.9 5.9 0 0 0 7.7.7l3 3a1 1 0 0 0 1.3 0c.4-.5.4-1 0-1.5zM8.1 13.8a4 4 0 0 1 0-5.7 4 4 0 0 1 5.7 0 4 4 0 0 1 0 5.7 4 4 0 0 1-5.7 0z"/></svg>' +
          "</button>",
    
        close:
          '<button data-fancybox-close class="fancybox-button fancybox-button--close" title="{{CLOSE}}">' +
          '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M12 10.6L6.6 5.2 5.2 6.6l5.4 5.4-5.4 5.4 1.4 1.4 5.4-5.4 5.4 5.4 1.4-1.4-5.4-5.4 5.4-5.4-1.4-1.4-5.4 5.4z"/></svg>' +
          "</button>",
    
        // Arrows
        arrowLeft:
          '<button data-fancybox-prev class="fancybox-button fancybox-button--arrow_left" title="{{PREV}}">' +
          '<div><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M11.28 15.7l-1.34 1.37L5 12l4.94-5.07 1.34 1.38-2.68 2.72H19v1.94H8.6z"/></svg></div>' +
          "</button>",
    
        arrowRight:
          '<button data-fancybox-next class="fancybox-button fancybox-button--arrow_right" title="{{NEXT}}">' +
          '<div><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M15.4 12.97l-2.68 2.72 1.34 1.38L19 12l-4.94-5.07-1.34 1.38 2.68 2.72H5v1.94z"/></svg></div>' +
          "</button>",
    
        // This small close button will be appended to your html/inline/ajax content by default,
        // if "smallBtn" option is not set to false
        smallBtn:
          '<button type="button" data-fancybox-close class="fancybox-button fancybox-close-small" title="{{CLOSE}}">' +
          '<svg xmlns="http://www.w3.org/2000/svg" version="1" viewBox="0 0 24 24"><path d="M13 12l5-5-1-1-5 5-5-5-1 1 5 5-5 5 1 1 5-5 5 5 1-1z"/></svg>' +
          "</button>"
      },
    
      // Container is injected into this element
      parentEl: "body",
    
      // Hide browser vertical scrollbars; use at your own risk
      hideScrollbar: true,
    
      // Focus handling
      // ==============
    
      // Try to focus on the first focusable element after opening
      autoFocus: true,
    
      // Put focus back to active element after closing
      backFocus: true,
    
      // Do not let user to focus on element outside modal content
      trapFocus: true,
    
      // Module specific options
      // =======================
    
      fullScreen: {
        autoStart: false
      },
    
      // Set `touch: false` to disable panning/swiping
      touch: {
        vertical: true, // Allow to drag content vertically
        momentum: true // Continue movement after releasing mouse/touch when panning
      },
    
      // Hash value when initializing manually,
      // set `false` to disable hash change
      hash: null,
    
      // Customize or add new media types
      // Example:
      /*
        media : {
          youtube : {
            params : {
              autoplay : 0
            }
          }
        }
      */
      media: {},
    
      slideShow: {
        autoStart: false,
        speed: 3000
      },
    
      thumbs: {
        autoStart: false, // Display thumbnails on opening
        hideOnClose: true, // Hide thumbnail grid when closing animation starts
        parentEl: ".fancybox-container", // Container is injected into this element
        axis: "y" // Vertical (y) or horizontal (x) scrolling
      },
    
      // Use mousewheel to navigate gallery
      // If 'auto' - enabled for images only
      wheel: "auto",
    
      // Callbacks
      //==========
    
      // See Documentation/API/Events for more information
      // Example:
      /*
        afterShow: function( instance, current ) {
          console.info( 'Clicked element:' );
          console.info( current.opts.$orig );
        }
      */
    
      onInit: $.noop, // When instance has been initialized
    
      beforeLoad: $.noop, // Before the content of a slide is being loaded
      afterLoad: $.noop, // When the content of a slide is done loading
    
      beforeShow: $.noop, // Before open animation starts
      afterShow: $.noop, // When content is done loading and animating
    
      beforeClose: $.noop, // Before the instance attempts to close. Return false to cancel the close.
      afterClose: $.noop, // After instance has been closed
    
      onActivate: $.noop, // When instance is brought to front
      onDeactivate: $.noop, // When other instance has been activated
    
      // Interaction
      // ===========
    
      // Use options below to customize taken action when user clicks or double clicks on the fancyBox area,
      // each option can be string or method that returns value.
      //
      // Possible values:
      //   "close"           - close instance
      //   "next"            - move to next gallery item
      //   "nextOrClose"     - move to next gallery item or close if gallery has only one item
      //   "toggleControls"  - show/hide controls
      //   "zoom"            - zoom image (if loaded)
      //   false             - do nothing
    
      // Clicked on the content
      clickContent: function(current, event) {
        return current.type === "image" ? "zoom" : false;
      },
    
      // Clicked on the slide
      clickSlide: "close",
    
      // Clicked on the background (backdrop) element;
      // if you have not changed the layout, then most likely you need to use `clickSlide` option
      clickOutside: "close",
    
      // Same as previous two, but for double click
      dblclickContent: false,
      dblclickSlide: false,
      dblclickOutside: false,
    
      // Custom options when mobile device is detected
      // =============================================
    
      mobile: {
        preventCaptionOverlap: false,
        idleTime: false,
        clickContent: function(current, event) {
          return current.type === "image" ? "toggleControls" : false;
        },
        clickSlide: function(current, event) {
          return current.type === "image" ? "toggleControls" : "close";
        },
        dblclickContent: function(current, event) {
          return current.type === "image" ? "zoom" : false;
        },
        dblclickSlide: function(current, event) {
          return current.type === "image" ? "zoom" : false;
        }
      },
    
      // Internationalization
      // ====================
    
      lang: "en",
      i18n: {
        en: {
          CLOSE: "Close",
          NEXT: "Next",
          PREV: "Previous",
          ERROR:
            "The requested content cannot be loaded. <br/> Please try again later.",
          PLAY_START: "Start slideshow",
          PLAY_STOP: "Pause slideshow",
          FULL_SCREEN: "Full screen",
          THUMBS: "Thumbnails",
          DOWNLOAD: "Download",
          SHARE: "Share",
          ZOOM: "Zoom"
        },
        de: {
          CLOSE: "Schliessen",
          NEXT: "Weiter",
          PREV: "Zurück",
          ERROR:
            "Die angeforderten Daten konnten nicht geladen werden. <br/> Bitte versuchen Sie es später nochmal.",
          PLAY_START: "Diaschau starten",
          PLAY_STOP: "Diaschau beenden",
          FULL_SCREEN: "Vollbild",
          THUMBS: "Vorschaubilder",
          DOWNLOAD: "Herunterladen",
          SHARE: "Teilen",
          ZOOM: "Maßstab"
        }
      }
    };
    
    $('[data-fancybox="images"]').fancybox(defaults);
    
    /*
    $('[data-fancybox="images"]').fancybox({
      afterLoad : function(instance, current) {
        var pixelRatio = window.devicePixelRatio || 1;
    
        if ( pixelRatio > 1.5 ) {
          current.width  = current.width  / pixelRatio;
          current.height = current.height / pixelRatio;
        }
      }
    });
    */
    
    // old syntax fancybox 2
    $(".various").fancybox({
      /*
        maxWidth	: 800,
            maxHeight	: 600,
            fitToView	: false,
            width		: '70%',
            height		: '70%',
            autoSize	: false,
            closeClick	: false,
            openEffect	: 'none',
            closeEffect	: 'none'
        */
      defaults
    });


    
    // Add new paragraph (various - inline example)
    $("#add_paragraph").click(function() {
      $(this)
        .parent()
        .next()
        .clone()
        .appendTo($(this).parent());
      $.fancybox.update();
    });
    
    
    
    // Step 1: Create reusable jQuery plugin
    // =====================================
    
    $.fancyConfirm = function( opts ) {
      opts  = $.extend( true, {
        title     : 'Are you sure?',
        message   : '',
        okButton  : 'OK',
        noButton  : 'Cancel',
        callback  : $.noop
      }, opts || {} );
    
      $.fancybox.open({
        type : 'html',
        src  :
        '<div class="fc-content">' +
        '<h3>' + opts.title   + '</h3>' +
        '<p>'  + opts.message + '</p>' +
        '<p class="tright">' +
        '<a data-value="0" data-fancybox-close>' + opts.noButton + '</a>' +
        '<button data-value="1" data-fancybox-close class="btn">' + opts.okButton + '</button>' +
        '</p>' +
        '</div>',
        opts : {
          animationDuration : 350,
          animationEffect   : 'material',
          modal : true,
          baseTpl :
          '<div class="fancybox-container fc-container" role="dialog" tabindex="-1">' +
          '<div class="fancybox-bg"></div>' +
          '<div class="fancybox-inner">' +
          '<div class="fancybox-stage"></div>' +
          '</div>' +
          '</div>',
          afterClose : function( instance, current, e ) {
            var button = e ? e.target || e.currentTarget : null;
            var value  = button ? $(button).data('value') : 0;
    
            opts.callback( value );
          }
        }
      });
    }
    
    // Step 2: Start using it!
    // =======================
    
    $("#test_confirm").click(function() {
    
      // Open customized confirmation dialog window
      $.fancyConfirm({
        title     : "Use Google's location service?",
        message   : "Let Google help apps determine location. This means sending anonymous location data to Google, even when no apps are running",
        okButton  : 'Agree',
        noButton  : 'Disagree',
        callback  : function (value) {
          if (value) {
            $("#test_confirm_rez").html("Let's do this!");
    
          } else {
            $("#test_confirm_rez").html("Maybe later.");
          }
        }
      });
    
    });
    
    
    
    // Step 1: Create jQuery plugin
    // ============================
    
    $.fn.fancyMorph = function( opts ) {
    
      var Morphing = function( $btn, opts ) {
        var self = this;
    
        self.opts = $.extend({
          animationEffect : false,
          infobar    : false,
          buttons    : ['close'],
          smallBtn   : false,
          touch      : false,
          baseClass  : 'fancybox-morphing',
          afterClose : function() {
            self.close();
          }
        }, opts);
    
        self.init( $btn );
      };
    
      Morphing.prototype.init = function( $btn ) {
        var self = this;
    
        self.$btn = $btn.addClass('morphing-btn');
    
        self.$clone = $('<div class="morphing-btn-clone" />')
          .hide()
          .insertAfter( $btn );
    
        // Add wrapping element and set initial width used for positioning
        $btn.wrap( '<span class="morphing-btn-wrap"></span>' ).on('click', function(e) {
          e.preventDefault();
    
          self.start();
        });
    
      };
    
      Morphing.prototype.start = function() {
        var self = this;
    
        if ( self.$btn.hasClass('morphing-btn_circle') ) {
          return;
        }
    
        // Set initial width, because it is not possible to start CSS transition from "auto"
        self.$btn.width( self.$btn.width() ).parent().width( self.$btn.outerWidth() );
    
        self.$btn.off('.fm').on("transitionend.fm webkitTransitionEnd.fm oTransitionEnd.fm MSTransitionEnd.fm", function(e) {
    
          if ( e.originalEvent.propertyName === 'width' ) {
            self.$btn.off('.fm');
    
            self.animateBg();
          }
    
        }).addClass('morphing-btn_circle');
    
      };
    
      Morphing.prototype.animateBg = function() {
        var self = this;
    
        self.scaleBg();
    
        self.$clone.show();
    
        // Trigger repaint
        self.$clone[0].offsetHeight;
    
        self.$clone.off('.fm').on("transitionend.fm webkitTransitionEnd.fm oTransitionEnd.fm MSTransitionEnd.fm", function(e) {
          self.$clone.off('.fm');
    
          self.complete();
    
        }).addClass('morphing-btn-clone_visible');
      };
    
      Morphing.prototype.scaleBg = function() {
        var self = this;
    
        var $clone = self.$clone;
        var scale  = self.getScale();
        var $btn   = self.$btn;
        var pos    = $btn.offset();
    
        $clone.css({
          top       : pos.top  + $btn.outerHeight() * 0.5 - ( $btn.outerHeight() * scale * 0.5 ) - $(window).scrollTop(),
          left      : pos.left + $btn.outerWidth()  * 0.5 - ( $btn.outerWidth()  * scale * 0.5 ) - $(window).scrollLeft(),
          width     : $btn.outerWidth()  * scale,
          height    : $btn.outerHeight() * scale,
          transform : 'scale(' + ( 1 / scale ) + ')'
        });
      };
    
      Morphing.prototype.getScale = function() {
        var $btn    = this.$btn,
            radius  = $btn.outerWidth() * 0.5,
            left    = $btn.offset().left + radius - $(window).scrollLeft(),
            top     = $btn.offset().top  + radius - $(window).scrollTop(),
            windowW = $(window).width(),
            windowH = $(window).height();
    
        var maxDistHor  = ( left > windowW / 2 ) ? left : ( windowW - left ),
            maxDistVert = ( top > windowH / 2 )  ? top  : ( windowH - top );
    
        return Math.ceil(Math.sqrt( Math.pow( maxDistHor, 2 ) + Math.pow( maxDistVert, 2 ) ) / radius );
      };
    
      Morphing.prototype.complete = function() {
        var self = this;
        var $btn = self.$btn;
    
        $.fancybox.open({ src : $btn.data('src') || $btn.attr('href') }, self.opts);
    
        $(window).on('resize.fm', function() {
          //self.scaleBg();
        });
      };
    
      Morphing.prototype.close = function() {
        var self   = this;
        var $clone = self.$clone;
    
        self.scaleBg();
    
        $clone.one('transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd', function(e) {
          $clone.hide();
    
          self.$btn.removeClass('morphing-btn_circle');
        });
    
        $clone.removeClass('morphing-btn-clone_visible');
    
        $(window).off('resize.fm');
      };
    
      // Init
      this.each(function() {
        var $this = $(this);
    
        if ( !$this.data("morphing") ) {
          $this.data( "morphing", new Morphing( $this, opts ) );
        }
    
      });
    
      return this;
    };
    
    
    // Step 2: Start using it!
    // =======================
    
    $("[data-morphing]").fancyMorph({
      hash : 'morphing'
    });
    
    
    
