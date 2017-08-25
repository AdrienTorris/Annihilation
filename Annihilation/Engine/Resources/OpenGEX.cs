using System;

namespace Engine.Resources
{
    public static class OpenGEX
    {
        // Structure types
        private const string StructureMetric = "mtrc";
        private const string StructureName = "name";
        private const string StructureObjectRef = "obrf";
        private const string StructureMaterialRef = "mtrf";
        private const string StructureMatrix = "mtrx";
        private const string StructureTransform = "xfrm";
        private const string StructureTranslation = "xslt";
        private const string StructureRotation = "rota";
        private const string StructureScale = "scal";
        private const string StructureMorphWeight = "mwgt";
        private const string StructureNode = "node";
        private const string StructureBoneNode = "bnnd";
        private const string StructureGeometryNode = "gmnd";
        private const string StructureLightNode = "ltnd";
        private const string StructureCameraNode = "cmnd";
        private const string StructureVertexArray = "vert";
        private const string StructureIndexArray = "indx";
        private const string StructureBoneRefArray = "bref";
        private const string StructureBoneCountArray = "bcnt";
        private const string StructureBoneIndexArray = "bidx";
        private const string StructureBoneWeightArray = "bwgt";
        private const string StructureSkeleton = "skel";
        private const string StructureSkin = "skin";
        private const string StructureMorph = "mrph";
        private const string StructureMesh = "mesh";
        private const string StructureObject = "objc";
        private const string StructureGeometryObject = "gmob";
        private const string StructureLightObject = "ltob";
        private const string StructureCameraObject = "cmob";
        private const string StructureAttrib = "attr";
        private const string StructureParam = "parm";
        private const string StructureColor = "colr";
        private const string StructureTexture = "txtr";
        private const string StructureAtten = "attn";
        private const string StructureMaterial = "matl";
        private const string StructureKey = "key ";
        private const string StructureCurve = "curv";
        private const string StructureTime = "time";
        private const string StructureValue = "valu";
        private const string StructureTrack = "trac";
        private const string StructureAnimation = "anim";
        private const string StructureClip = "clip";
        private const string StructureExtension = "extn";

        // Data types
        private const string DataInvalidUpDirection = "ivud";
        private const string DataInvalidForwardDirection = "ivfd";
        private const string DataInvalidTranslationKind = "ivtk";
        private const string DataInvalidRotationKind = "ivrk";
        private const string DataInvalidScaleKind = "ivsk";
        private const string DataDuplicateLod = "dlod";
        private const string DataMissingLodSkin = "mlsk";
        private const string DataMissingLodMorph = "mlmp";
        private const string DataDuplicateMorph = "dmph";
        private const string DataUndefinedLightType = "ivlt";
        private const string DataUndefinedCurve = "udcv";
        private const string DataUndefinedAtten = "udan";
        private const string DataDuplicateVertexArray = "dpva";
        private const string DataPositionArrayRequired = "parq";
        private const string DataVertexCountUnsupported = "vcus";
        private const string DataIndexValueUnsupported = "ivus";
        private const string DataIndexArrayRequired = "iarq";
        private const string DataVertexCountMismatch = "vcmm";
        private const string DataBoneCountMismatch = "bcmm";
        private const string DataBoneWeightCountMismatch = "bwcm";
        private const string DataInvalidBoneRef = "ivbr";
        private const string DataInvalidObjectRef = "ivor";
        private const string DataInvalidMaterialRef = "ivmr";
        private const string DataMaterialIndexUnsupported = "mius";
        private const string DataDuplicateMaterialRef = "dprf";
        private const string DataMissingMaterialRef = "msrf";
        private const string DataTargetRefNotLocal = "trnl";
        private const string DataInvalidTargetStruct = "ivts";
        private const string DataInvalidKeyKind = "ivkk";
        private const string DataInvalidCurveType = "ivct";
        private const string DataKeyCountMismatch = "kycm";
        private const string DataEmptyKeyStructure = "emky";
    }
}