<template>
	<div :id="uid" class="block-body">
		<div class="row justify-content-end">
			<div class="col-2 text-right">
				<div class="form-group">
					<select class="form-control" v-model="model.alignment.value" v-on:change="onAlignmentChange">
						<option value="0" :selected="`${isSelected(0)}`">Left</option>
						<option value="1" :selected="`${isSelected(1)}`">Center</option>
						<option value="2" :selected="`${isSelected(2)}`">Right</option>
					</select>
				</div>
			</div>
		</div>
		<div class="heading-block">
			<div :class="`heading ${getAlignment()}`">
				<h2 contenteditable="true" v-html="model.heading.value" v-on:blur="onHeadingBlur" :data-placeholder="placeholder.heading"></h2>
				<p contenteditable="true" v-html="model.description.value" v-on:blur="onDescriptionBlur" :data-placeholder="placeholder.description"></p>
			</div>
		</div>
	</div>
</template>


<script>
	export default {
		props: ["uid", "model"],
		data: function () {
			return {
				placeholder: {
					heading: null,
					description: null,
					alignment: null
				}
			}
		},
		methods: {
			onAlignmentChange: function (e) {
				this.model.alignment.value = e.target.value;
			},
			onHeadingBlur: function (e) {
				this.model.heading.value = e.target.innerText;
			},
			onDescriptionBlur: function (e) {
				this.model.description.value = e.target.innerText;
				// Tell parent that heading has been updated
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
				if (this.model.alignment.value == 0)
					return "text-left";
				if (this.model.alignment.value == 2)
					return "text-right";
				return "text-center";
			},
			isSelected: function (alignment) {
				if (alignment == this.model.alignment.value)
					return true;
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
		}
	}
</script>
