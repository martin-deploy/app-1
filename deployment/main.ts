import { configMap } from "./config-map"
import { v4 as uuid } from "uuid"

let resources = [] as object[]

resources.push(configMap("hello", {
	hello: "martin",
	fromApp: "1",
	id: uuid()
}))

// @ts-ignore
resources.push(configMap("hello-env", <undefined>process.env))

console.log(JSON.stringify({ apiVersion: "v1", kind: "List", items: resources }, undefined, 2))

