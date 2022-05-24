<template>
	<div class="block-body rounded clearfix">
		<div>
			<img class="card-img-top" style="cursor: pointer" v-on:click.prevent="selectImage" :src="image">
			<div class="card-body">
				<h3 contenteditable="true" class="card-title" :class="{ 'text-secondary': !hasTitle }" v-html="title" v-on:blur="onTitleBlur"></h3>
				<h5 contenteditable="true" class="card-subtitle" :class="{ 'text-secondary': !hasSubTitle }" v-html="subTitle" v-on:blur="onSubTitleBlur"></h5>
				<p contenteditable="true" :class="{ 'text-secondary': !hasContent }" v-html="content" v-on:blur="onContentBlur"></p>
				<div contenteditable="true" class="btn btn-outline-primary" style="cursor: text" v-html="buttonText" v-on:blur="onButtonBlur"></div>
				<div class="row mt-3">
					<div class="col">
						<div class="btn-group">
							<button v-on:click.prevent="selectPage" class="btn btn-primary text-center">
								<i class="fas fa-link"></i>
							</button>
							<button v-on:click.prevent="removePage" class="btn btn-danger text-center">
								<i class="fas fa-unlink"></i>
							</button>
						</div>
						<span v-if="hasLinkTitle">
							{{ linkTitle }}
						</span>
						<span v-else>
							Please select a page to link to.
						</span>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	export default {
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
						publicUrl: media.publicUrl,
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

					fetch(piranha.baseUrl + "manager/api/page/info/" + page.id)
						.then(function (response) { return response.json(); })
						.then(function (result) {
							self.model.link.id = result.id;
							self.model.link.page = result;
						})
						.catch(function (error) { console.log("error:", error); });
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
		}
	}
</script>
