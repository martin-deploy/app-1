import { configMap } from "./config-map"

let resources = [] as object[]

resources.push(configMap("hello", {
	hello: "martin",
	fromApp: "1"
}))

// @ts-ignore
resources.push(configMap("hello-env", <undefined>process.env))

console.log(JSON.stringify({ apiVersion: "v1", kind: "List", items: resources }, undefined, 2))

