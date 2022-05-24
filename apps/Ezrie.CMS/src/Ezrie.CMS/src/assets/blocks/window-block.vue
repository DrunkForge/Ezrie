<template>
	<div class="block-body" :class="{ empty: isEmpty }">

		<div class="window-block">
			<div class="window-block-image bg-fixed" :style="bgStyle">
				<div class="overlay-content" contenteditable="true" :id="uid" v-html="body" v-on:blur="onBodyBlur"></div>
			</div>
		</div>

		<div class="form-inline my-3">
			<label class="mx-1">Window Height</label>
			<input class="form-control mx-1" type="number" v-model="height" v-on:blur="onHeightBlur">
			<button class="btn btn-primary mx-1" v-on:click.prevent="selectImage"><i class="fas fa-image"></i> Browse</button>
		</div>

	</div>
</template>

<script>

	export default {
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
						publicUrl: media.publicUrl,
					};
				}
			},
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
		}
	}
</script>
