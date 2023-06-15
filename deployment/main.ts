import { configMap } from "./config-map"
import { v4 as uuid } from "uuid"

declare const process: { env: Record<string, string> }

const appSourceRepoUrl                  = process.env.ARGOCD_APP_SOURCE_REPO_URL        // "https://github.com/martin-deploy/app-1.git"
const appSourceTargetRevision           = process.env.ARGOCD_APP_SOURCE_TARGET_REVISION // "master"
const appSourceTargetRevisionCommitHash = process.env.ARGOCD_APP_REVISION               // "c5de981eff6ebbaa2493bd2929522f7f0d100add"   <- Commit hash of target revision
const appSourcePath                     = process.env.ARGOCD_APP_SOURCE_PATH            // "deployment"
const appName                           = process.env.ARGOCD_APP_NAME                   // "martin-app-1"                               <- From Argo CD application's metadata.name
const appNamespace                      = process.env.ARGOCD_APP_NAMESPACE              // "martin-prod"                                <- From Argo CD application's spec.destination.namespace
const appParameters                     = JSON.parse(process.env.ARGOCD_APP_PARAMETERS) // "null"                                       <- From Argo CD application's spec.source.plugin (as a JSON string)

let resources = [] as object[]

resources.push(configMap("hello", {
	hello: "martin",
	fromApp: "1",
	id: uuid()
}))

resources.push(configMap("hello-argo-env", {
	appSourceRepoUrl,
	appSourceTargetRevision,
	appSourceTargetRevisionCommitHash,
	appSourcePath,
	appName,
	appNamespace,
	appParameters
}))

console.log(JSON.stringify({ apiVersion: "v1", kind: "List", items: resources }, undefined, 2))
