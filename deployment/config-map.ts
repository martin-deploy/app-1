export function configMap(name: string, data: Record<string, string>) {
	return {
		apiVersion: "v1",
		kind: "ConfigMap",
		metadata: {
			name: name
		},
		data: data
	}
}
