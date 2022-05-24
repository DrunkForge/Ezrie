Vue.component("accordion-panel-block", {
  props: ["uid", "toolbar", "model"],
  data: function () {
    return {
      titleId: null,
      title: this.model.title.value,
      bodyId: null,
      body: this.model.body.value,
      placeholder: {
        title: null,
        body: null
      }
    };
  },
  methods: {
    onTitleBlur: function (e) {
      this.model.title.value = e.target.innerText; // Tell parent that title has been updated

      var title = this.model.title.value.replace(/(<([^>]+)>)/ig, "");

      if (title.length > 40) {
        title = title.substring(0, 40) + "...";
      }

      this.$emit('update-title', {
        uid: this.uid,
        title: title
      });
    },
    onBodyBlur: function (e) {
      this.model.body.value = tinyMCE.activeEditor.getContent();
    },
    onChange: function (data) {
      console.log(data);
      if (data != '<br data-mce-bogus="1">') this.model.body.value = data;
    }
  },
  computed: {
    isEmpty: function () {
      return this.model.title.value == null && this.model.body.value == null;
    }
  },
  created: function () {
    this.placeholder = {
      title: "Fugit anim consectetuer leo temporibus",
      body: "Repellat itaque atque sagittis lectus, volutpat necessitatibus praesentium vestibulum atque quidem quaerat sapien. Ex dui aliqua orci dictum integer! est elit. Ipsam numquam. Accumsan explicabo vulputate quam aspernatur platea inventore iure vel ante,"
    };
    this.titleId = this.uid + 't';
    this.bodyId = this.uid + 'b';
  },
  mounted: function () {
    piranha.editor.addInline(this.bodyId, this.toolbar, this.onChange);
  },
  beforeDestroy: function () {
    piranha.editor.remove(this.bodyId);
  },
  template: "\n<div :id=\"uid\" class=\"block-body\" :class=\"{ empty: isEmpty }\">\n\t<p contenteditable=\"true\" class=\"panel-title\" :id=\"titleId\" v-html=\"title\" v-on:blur=\"onTitleBlur\" :data-placeholder=\"placeholder.title\"></p>\n\t<p contenteditable=\"true\" class=\"panel-body\" :id=\"bodyId\" v-html=\"body\" v-on:blur=\"onBodyBlur\" :data-placeholder=\"placeholder.body\"></p>\n</div>\n"
});
Vue.component("card-block", {
  props: ["uid", "model"],
  methods: {
    onTitleBlur: function (e) {
      this.model.title.value = e.target.innerText;
    },
    onSubTitleBlur: function (e) {
      this.model.subTitle.value = e.target.innerText;
    },
    onContentBlur: function (e) {
      this.model.content.value = e.target.innerText;
    },
    onButtonBlur: function (e) {
      this.model.button.value = e.target.innerText;
    },
    selectImage: function () {
      if (this.model.image.media != null) {
        piranha.mediapicker.open(this.updateImage, "Image", this.model.image.media.folderId);
      } else {
        piranha.mediapicker.openCurrentFolder(this.updateImage, "Image");
      }
    },
    removeImage: function () {
      this.model.image.id = null;
      this.model.image.media = null;
    },
    updateImage: function (media) {
      if (media.type === "Image") {
        this.model.image.id = media.id;
        this.model.image.media = {
          id: media.id,
          folderId: media.folderId,
          type: media.type,
          filename: media.filename,
          contentType: media.contentType,
          publicUrl: media.publicUrl
        };
      }
    },
    selectPage: function () {
      piranha.pagepicker.open(this.updatePage);
    },
    removePage: function () {
      this.model.link.id = null;
      this.model.link.page = null;
    },
    updatePage: function (page) {
      if (page !== null) {
        var self = this;
        fetch(piranha.baseUrl + "manager/api/page/info/" + page.id).then(function (response) {
          return response.json();
        }).then(function (result) {
          self.model.link.id = result.id;
          self.model.link.page = result;
        }).catch(function (error) {
          console.log("error:", error);
        });
      }
    }
  },
  computed: {
    image: function () {
      if (this.hasImage) {
        return piranha.baseUrl + "manager/api/media/url/" + this.model.image.media.id + "/1080/720";
      } else {
        return piranha.utils.formatUrl("~/manager/assets/img/empty-image.png");
      }
    },
    hasImage: function () {
      return this.model.image.page !== null && this.model.image.media !== null;
    },
    title: function () {
      if (this.hasTitle) {
        return this.model.title.value;
      }

      return "I Need A Title...";
    },
    hasTitle: function () {
      return this.model.title !== null && !piranha.utils.isEmptyText(this.model.title.value);
    },
    subTitle: function () {
      if (this.hasSubTitle) {
        return this.model.subTitle.value;
      }

      return "The subTitle is optional.";
    },
    hasSubTitle: function () {
      return this.model.subTitle !== null && !piranha.utils.isEmptyText(this.model.subTitle.value);
    },
    content: function () {
      if (this.hasContent) {
        return this.model.content.value;
      }

      return "Some random giberish goes here. Maybe a bit of snarkiness as well.";
    },
    hasContent: function () {
      return this.model.content !== null && !piranha.utils.isEmptyText(this.model.content.value);
    },
    linkTitle: function () {
      if (this.hasLinkTitle) {
        return this.model.link.page.title;
      }

      return "";
    },
    hasLinkTitle: function () {
      return this.model.link.page !== null;
    },
    buttonText: function () {
      if (this.hasButtonText) {
        return this.model.button.value;
      }

      return "Click Me!";
    },
    hasButtonText: function () {
      return !piranha.utils.isEmptyText(this.model.button.value);
    }
  },
  mounted: function () {
    this.model.getTitle = function () {
      if (this.model.link.page !== null) {
        return this.model.link.page.title;
      } else {
        return "No page selected";
      }
    };
  },
  template: "\n<div class=\"block-body rounded clearfix\">\n\t<div>\n\t\t<img class=\"card-img-top\" style=\"cursor: pointer\" v-on:click.prevent=\"selectImage\" :src=\"image\">\n\t\t<div class=\"card-body\">\n\t\t\t<h3 contenteditable=\"true\" class=\"card-title\" :class=\"{ 'text-secondary': !hasTitle }\" v-html=\"title\" v-on:blur=\"onTitleBlur\"></h3>\n\t\t\t<h5 contenteditable=\"true\" class=\"card-subtitle\" :class=\"{ 'text-secondary': !hasSubTitle }\" v-html=\"subTitle\" v-on:blur=\"onSubTitleBlur\"></h5>\n\t\t\t<p contenteditable=\"true\" :class=\"{ 'text-secondary': !hasContent }\" v-html=\"content\" v-on:blur=\"onContentBlur\"></p>\n\t\t\t<div contenteditable=\"true\" class=\"btn btn-outline-primary\" style=\"cursor: text\" v-html=\"buttonText\" v-on:blur=\"onButtonBlur\"></div>\n\t\t\t<div class=\"row mt-3\">\n\t\t\t\t<div class=\"col\">\n\t\t\t\t\t<div class=\"btn-group\">\n\t\t\t\t\t\t<button v-on:click.prevent=\"selectPage\" class=\"btn btn-primary text-center\">\n\t\t\t\t\t\t\t<i class=\"fas fa-link\"></i>\n\t\t\t\t\t\t</button>\n\t\t\t\t\t\t<button v-on:click.prevent=\"removePage\" class=\"btn btn-danger text-center\">\n\t\t\t\t\t\t\t<i class=\"fas fa-unlink\"></i>\n\t\t\t\t\t\t</button>\n\t\t\t\t\t</div>\n\t\t\t\t\t<span v-if=\"hasLinkTitle\">\n\t\t\t\t\t\t{{ linkTitle }}\n\t\t\t\t\t</span>\n\t\t\t\t\t<span v-else>\n\t\t\t\t\t\tPlease select a page to link to.\n\t\t\t\t\t</span>\n\t\t\t\t</div>\n\t\t\t</div>\n\t\t</div>\n\t</div>\n</div>\n"
});
Vue.component("heading-block", {
  props: ["uid", "model"],
  data: function () {
    return {
      placeholder: {
        heading: null,
        description: null,
        alignment: null
      }
    };
  },
  methods: {
    onAlignmentChange: function (e) {
      this.model.alignment.value = e.target.value;
    },
    onHeadingBlur: function (e) {
      this.model.heading.value = e.target.innerText;
    },
    onDescriptionBlur: function (e) {
      this.model.description.value = e.target.innerText; // Tell parent that heading has been updated

      var title = this.model.heading.value.replace(/(<([^>]+)>)/ig, "");

      if (title.length > 40) {
        title = heading.substring(0, 40) + "...";
      }

      this.$emit('update-title', {
        uid: this.uid,
        title: title
      });
    },
    getAlignment: function () {
      if (this.model.alignment.value == 0) return "text-left";
      if (this.model.alignment.value == 2) return "text-right";
      return "text-center";
    },
    isSelected: function (alignment) {
      if (alignment == this.model.alignment.value) return true;
      return false;
    }
  },
  computed: {
    isEmpty: function () {
      return this.model.heading.value == null;
    }
  },
  created: function () {
    this.placeholder = {
      heading: "Section Heading",
      description: "Section Description (Optional)",
      alignment: 1
    };
  },
  template: "\n<div :id=\"uid\" class=\"block-body\">\n\t<div class=\"row justify-content-end\">\n\t\t<div class=\"col-2 text-right\">\n\t\t\t<div class=\"form-group\">\n\t\t\t\t<select class=\"form-control\" v-model=\"model.alignment.value\" v-on:change=\"onAlignmentChange\">\n\t\t\t\t\t<option value=\"0\" :selected=\"`${isSelected(0)}`\">Left</option>\n\t\t\t\t\t<option value=\"1\" :selected=\"`${isSelected(1)}`\">Center</option>\n\t\t\t\t\t<option value=\"2\" :selected=\"`${isSelected(2)}`\">Right</option>\n\t\t\t\t</select>\n\t\t\t</div>\n\t\t</div>\n\t</div>\n\t<div class=\"heading-block\">\n\t\t<div :class=\"`heading ${getAlignment()}`\">\n\t\t\t<h2 contenteditable=\"true\" v-html=\"model.heading.value\" v-on:blur=\"onHeadingBlur\" :data-placeholder=\"placeholder.heading\"></h2>\n\t\t\t<p contenteditable=\"true\" v-html=\"model.description.value\" v-on:blur=\"onDescriptionBlur\" :data-placeholder=\"placeholder.description\"></p>\n\t\t</div>\n\t</div>\n</div>\n"
});
Vue.component("window-block", {
  props: ["uid", "toolbar", "model"],
  data: function () {
    return {
      body: this.model.body.value,
      height: this.model.height.value
    };
  },
  methods: {
    onBodyBlur: function (e) {
      this.model.body.value = tinyMCE.activeEditor.getContent();
    },
    onBodyChange: function (data) {
      this.model.body.value = data;
    },
    onHeightBlur: function (e) {
      this.model.height.value = e.target.value;
    },
    onHeightChange: function (data) {
      this.model.height.value = e.target.innerText;
    },
    selectImage: function () {
      if (this.model.image.media != null) {
        piranha.mediapicker.open(this.updateImage, "Image", this.model.image.media.folderId);
      } else {
        piranha.mediapicker.openCurrentFolder(this.updateImage, "Image");
      }
    },
    removeImage: function () {
      this.model.image.id = null;
      this.model.image.media = null;
    },
    updateImage: function (media) {
      if (media.type === "Image") {
        this.model.image.id = media.id;
        this.model.image.media = {
          id: media.id,
          folderId: media.folderId,
          type: media.type,
          filename: media.filename,
          contentType: media.contentType,
          publicUrl: media.publicUrl
        };
      }
    }
  },
  computed: {
    isEmpty: function () {
      return piranha.utils.isEmptyHtml(this.model.body.value);
    },
    bgStyle: function () {
      if (this.hasImage) {
        return `height: ${this.model.height.value}; background-image:url(${piranha.baseUrl}manager/api/media/url/${this.model.image.media.id}`;
      } else {
        return `height: ${this.model.height.value}; background-image:url(${piranha.utils.formatUrl("~/manager/assets/img/empty-image.png")})`;
      }
    },
    image: function () {
      if (this.hasImage) {
        return `background-image:url(${piranha.baseUrl}manager/api/media/url/${this.model.image.media.id}/1080/720`;
      } else {
        return piranha.utils.formatUrl("~/manager/assets/img/empty-image.png");
      }
    },
    hasImage: function () {
      return this.model.image.page !== null && this.model.image.media !== null;
    }
  },
  mounted: function () {
    piranha.editor.addInline(this.uid, this.toolbar, this.onBodyChange);
  },
  beforeDestroy: function () {
    piranha.editor.remove(this.uid);
  },
  template: "\n<div class=\"block-body\" :class=\"{ empty: isEmpty }\">\n\n\t<div class=\"window-block\">\n\t\t<div class=\"window-block-image bg-fixed\" :style=\"bgStyle\">\n\t\t\t<div class=\"overlay-content\" contenteditable=\"true\" :id=\"uid\" v-html=\"body\" v-on:blur=\"onBodyBlur\"></div>\n\t\t</div>\n\t</div>\n\n\t<div class=\"form-inline my-3\">\n\t\t<label class=\"mx-1\">Window Height</label>\n\t\t<input class=\"form-control mx-1\" type=\"number\" v-model=\"height\" v-on:blur=\"onHeightBlur\">\n\t\t<button class=\"btn btn-primary mx-1\" v-on:click.prevent=\"selectImage\"><i class=\"fas fa-image\"></i> Browse</button>\n\t</div>\n\n</div>\n"
});