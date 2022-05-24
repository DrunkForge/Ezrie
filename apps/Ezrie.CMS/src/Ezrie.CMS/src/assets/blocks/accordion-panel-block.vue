<template>
	<div :id="uid" class="block-body" :class="{ empty: isEmpty }">
		<p contenteditable="true" class="panel-title" :id="titleId" v-html="title" v-on:blur="onTitleBlur" :data-placeholder="placeholder.title"></p>
		<p contenteditable="true" class="panel-body" :id="bodyId" v-html="body" v-on:blur="onBodyBlur" :data-placeholder="placeholder.body"></p>
	</div>
</template>

<script>
	export default {
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
			}
		},
		methods: {
			onTitleBlur: function (e) {
				this.model.title.value = e.target.innerText;

				// Tell parent that title has been updated
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
				if (data != '<br data-mce-bogus="1">')
					this.model.body.value = data;
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
		}
	}
</script>
